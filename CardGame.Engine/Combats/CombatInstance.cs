using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Ai;
using CardGame.Engine.Combats.Cards;
using CardGame.Engine.Combats.Exceptions;
using CardGame.Engine.Combats.History;
using CardGame.Engine.Combats.Logs;
using CardGame.Engine.Combats.Utils;

namespace CardGame.Engine.Combats;

public class CombatInstance : IDisposable
{
    public CombatInstance(IReadOnlyList<Character> leftCharacters, IReadOnlyList<Character> rightCharacters, CombatOptions options)
    {
        Options = options;
        State = new CombatState(leftCharacters, rightCharacters, options.RandomSeed);

        options.RandomSeed = State.RandomSeed;

        History = new CombatHistory(new InitialCombatState(State.RandomSeed, leftCharacters, rightCharacters, options));
        Log = new CombatLog();

        State.EventHasBeenTriggered += OnAfterEventTriggered;

        StartGlobalTurn(1);
        StartSideTurn(Options.StartingSide);
    }

    public CombatOptions Options { get; }
    public CombatState State { get; }
    public CombatHistory History { get; }
    public CombatLog Log { get; }

    public CombatAi? LeftAi { get; private set; }

    public CombatAi? RightAi { get; private set; }

    public void Dispose()
    {
        State.EventHasBeenTriggered -= OnAfterEventTriggered;
    }

    public void PlayCard(CombatSide side, int index)
    {
        State.AssertOngoing();
        State.AssertSideCanPlay(side);

        CombatSideState sideState = State.GetSide(side);
        ActionCardInstance? card = sideState.Hand.ElementAtOrDefault(index);
        if (card == null)
        {
            throw new InvalidMoveException($"Could not find card at index {index}");
        }

        sideState.ReturnCardFromHandToDeck(index);

        using (Log.RecordEffectsOfPlayingCard(card))
        {
            card.Resolve();
        }

        History.RecordCardPlay(side, index);

        TryEndCombat();
    }

    public void EndTurn(CombatSide side)
    {
        State.AssertOngoing();
        State.AssertNotOver();
        State.AssertSideCanPlay(side);

        EndSideTurn(side);

        History.RecordEndTurn(side);

        CombatSide newSide = State.Side;
        if (State.Side == CombatSide.None)
        {
            StartGlobalTurn(State.Turn + 1);
            newSide = Options.StartingSide;
        }

        StartSideTurn(newSide);
    }

    public void SetAi(CombatSide side, CombatAiOptions options)
    {

        CombatAi ai = CombatAiFactory.CreateInstance(this, side, options);

        switch (side)
        {
            case CombatSide.Left:
                LeftAi = ai;
                break;
            case CombatSide.Right:
                RightAi = ai;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }

        History.RecordSetAi(side, options);
    }

    void StartGlobalTurn(int turn)
    {
        int turnAp = Math.Min(Options.StartingAp + turn - 1, Options.MaxAp);
        State.StartTurn(turn, turnAp);
    }

    void StartSideTurn(CombatSide side)
    {
        State.StartPhaseOfSide(side, CombatSideTurnPhase.StartOfTurn);

        if (TryEndCombat())
        {
            return;
        }

        State.StartPhaseOfSide(side, CombatSideTurnPhase.Draw);

        CombatSideState sideState = State.GetSide(side);
        int handSize = sideState.GetAllCharacters().Count() switch
        {
            1 => Options.HandSizeWithOneCharacter, 2 => Options.HandSizeWithBothCharacters,
            _ => throw new ArgumentOutOfRangeException()
        };
        while (sideState.Hand.Count < handSize && sideState.DrawRandomCardFromDeckToHand())
        {
            //
        }

        State.StartPhaseOfSide(side, CombatSideTurnPhase.Play);

        sideState.SetAp(State.MaxAp);

        RunAiIfNecessary();
    }

    void EndSideTurn(CombatSide side)
    {
        State.StartPhaseOfSide(side, CombatSideTurnPhase.EndOfTurn);

        if (TryEndCombat())
        {
            return;
        }

        CombatSide nextSide = GetNextSide(side);
        State.StartPhaseOfSide(nextSide, CombatSideTurnPhase.None);
    }

    void EndCombat(CombatSide winner)
    {
        State.EndCombat(winner);
    }

    bool CheckWinCondition(out CombatSide winner)
    {
        bool leftLost = !State.LeftSide.GetAllCharacters().Any();
        bool rightLost = !State.RightSide.GetAllCharacters().Any();

        if (leftLost && rightLost)
        {
            winner = CombatSide.None;
            return true;
        }

        if (leftLost)
        {
            winner = CombatSide.Right;
            return true;
        }

        if (rightLost)
        {
            winner = CombatSide.Left;
            return true;
        }

        winner = CombatSide.None;
        return false;
    }

    CombatSide GetNextSide(CombatSide side)
    {
        if (side == CombatSide.None)
        {
            return Options.StartingSide;
        }

        return side == Options.StartingSide ? side.OtherSide() : CombatSide.None;
    }

    void RemoveDeadCharactersIfAny()
    {
        RemoveDeadCharactersIfAny(State.LeftSide);
        RemoveDeadCharactersIfAny(State.RightSide);
    }

    static void RemoveDeadCharactersIfAny(CombatSideState side)
    {
        if (side.Back is { IsDead: true })
        {
            side.RemoveCharacterAndItsCards(CombatPosition.Back);
        }

        if (side.Front is { IsDead: true })
        {
            side.RemoveCharacterAndItsCards(CombatPosition.Front);
        }
    }

    bool TryEndCombat()
    {
        if (!State.Ongoing)
        {
            return true;
        }

        RemoveDeadCharactersIfAny();
        if (CheckWinCondition(out CombatSide winner))
        {
            EndCombat(winner);
            return true;
        }

        return false;
    }

    void RunAiIfNecessary()
    {
        CombatAi? ai = State.Side switch
        {
            CombatSide.Left => LeftAi,
            CombatSide.Right => RightAi,
            _ => null
        };

        if (ai != null)
        {
            CombatSide side = State.Side;
            try
            {
                ai.PlayTurn();
            }
            finally
            {
                if (State.Ongoing && State.Side == side)
                {
                    EndTurn(side);
                }
            }
        }
    }

    void OnAfterEventTriggered(object? sender, EventArgs e)
    {
        TryEndCombat();
    }
}
