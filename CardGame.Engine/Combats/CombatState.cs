using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats;

public class CombatState
{
    public CombatState(IReadOnlyList<Character> leftCharacters, IReadOnlyList<Character> rightCharacters, int? randomSeed = null)
    {
        Ongoing = false;
        Over = false;

        RandomSeed = randomSeed ?? Random.Shared.Next();
        Random random = new(RandomSeed);

        Turn = 0;
        MaxAp = 0;
        Side = CombatSide.None;
        Phase = CombatSideTurnPhase.None;
        Winner = CombatSide.None;

        LeftSide = new CombatSideState(this, CombatSide.Left, leftCharacters, new Random(random.Next()));
        RightSide = new CombatSideState(this, CombatSide.Right, rightCharacters, new Random(random.Next()));
    }

    public int RandomSeed { get; }
    public bool Ongoing { get; private set; }
    public bool Over { get; private set; }
    public int Turn { get; private set; }
    public int MaxAp { get; private set; }
    public CombatSide Side { get; private set; }
    public CombatSideTurnPhase Phase { get; private set; }
    public CombatSide Winner { get; private set; }

    public CombatSideState LeftSide { get; }
    public CombatSideState RightSide { get; }
    public CombatSideState CurrentSide => GetSide(Side);
    public CombatSideState OtherSide => GetSide(Side.OtherSide());

    public event EventHandler<TurnEventArgs>? TurnStarted;
    public event EventHandler<TurnEventArgs>? TurnEnded;
    public event EventHandler<PhaseEventArgs>? PhaseStarted;
    public event EventHandler<PhaseEventArgs>? PhaseEnded;
    public event EventHandler<CombatEndedEventArgs>? Ended;

    public event EventHandler? EventHasBeenTriggered;

    public CombatSideState GetSide(CombatSide side)
    {
        return side switch
        {
            CombatSide.Left => LeftSide,
            CombatSide.Right => RightSide,
            _ => throw new ArgumentOutOfRangeException(nameof(Side), Side, null)
        };
    }

    internal void StartTurn(int turn, int maxAp)
    {
        if (Ongoing)
        {
            InvokeEvent(() => TurnEnded?.Invoke(this, new TurnEventArgs(Turn)));
        }
        else
        {
            Ongoing = true;
            Over = false;
        }

        Turn = turn;
        MaxAp = maxAp;

        Side = CombatSide.None;

        InvokeEvent(() => TurnStarted?.Invoke(this, new TurnEventArgs(Turn)));
    }

    internal void StartPhaseOfSide(CombatSide side, CombatSideTurnPhase phase)
    {
        if (Phase != CombatSideTurnPhase.None)
        {
            InvokeEvent(() => PhaseEnded?.Invoke(this, new PhaseEventArgs(Turn, Side, Phase)));
        }


        Side = side;
        Phase = phase;

        if (side != CombatSide.None)
        {
            InvokeEvent(() => PhaseStarted?.Invoke(this, new PhaseEventArgs(Turn, Side, Phase)));
        }
    }

    internal void EndCombat(CombatSide winner)
    {
        Ongoing = false;
        Over = true;

        Side = CombatSide.None;
        Phase = CombatSideTurnPhase.None;

        Winner = winner;

        InvokeEvent(() => Ended?.Invoke(this, new CombatEndedEventArgs(Winner)));
    }

    void InvokeEvent(Action action)
    {
        action();
        EventHasBeenTriggered?.Invoke(this, EventArgs.Empty);
    }
}

public class CombatEndedEventArgs
{
    public CombatEndedEventArgs(CombatSide winner)
    {
        Winner = winner;
    }

    public CombatSide Winner { get; }
}

public class TurnEventArgs
{
    public TurnEventArgs(int turn)
    {
        Turn = turn;
    }

    int Turn { get; }
}

public class PhaseEventArgs
{
    public PhaseEventArgs(int turn, CombatSide side, CombatSideTurnPhase phase)
    {
        Turn = turn;
        Side = side;
        Phase = phase;
    }

    public int Turn { get; }
    public CombatSide Side { get; }
    public CombatSideTurnPhase Phase { get; }
}
