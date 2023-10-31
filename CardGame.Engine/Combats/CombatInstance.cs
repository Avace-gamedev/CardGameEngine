using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Resolve;
using CardGame.Engine.Extensions;

namespace CardGame.Engine.Combats;

public class CombatInstance
{
    readonly CombatSideInstance _leftSide;
    readonly CombatSideInstance _rightSide;

    public CombatInstance(IReadOnlyList<Character> leftCharacters, IReadOnlyList<Character> rightCharacters, CombatOptions options)
    {
        if (leftCharacters.Count == 0)
        {
            throw new Exception("Expected at least one character (left side)");
        }

        if (rightCharacters.Count == 0)
        {
            throw new Exception("Expected at least one character (right side)");
        }
        Options = options;

        Character? backLeftCharacter = leftCharacters.ElementAtOrDefault(1);
        Character? backRightCharacter = rightCharacters.ElementAtOrDefault(1);

        CharacterCombatState frontLeft = new(this, CombatSide.Left, leftCharacters[0]);
        CharacterCombatState? backLeft = backLeftCharacter == null ? null : new CharacterCombatState(this, CombatSide.Left, backLeftCharacter);
        CharacterCombatState frontRight = new(this, CombatSide.Right, rightCharacters[0]);
        CharacterCombatState? backRight = backRightCharacter == null ? null : new CharacterCombatState(this, CombatSide.Right, backRightCharacter);

        _leftSide = new CombatSideInstance(this, CombatSide.Left, frontLeft, backLeft);
        _rightSide = new CombatSideInstance(this, CombatSide.Right, frontRight, backRight);

        Ongoing = false;
        Over = false;

        Turn = 0;
        Side = CombatSide.None;
        SideTurnPhase = CombatSideTurnPhase.None;
    }

    CombatSideInstance CurrentSide =>
        Side switch
        {
            CombatSide.Left => _leftSide, CombatSide.Right => _rightSide,
            _ => throw new ArgumentOutOfRangeException(nameof(Side), Side, null)
        };

    public CombatOptions Options { get; }

    public bool Ongoing { get; private set; }
    public bool Over { get; private set; }
    public int Turn { get; private set; }
    public CombatSide Side { get; private set; }
    public CombatSideTurnPhase SideTurnPhase { get; private set; }
    public CombatSide Winner { get; private set; }

    public void Start()
    {
        AssertNotStarted();
        AssertNotOver();

        Turn = 1;
        Ongoing = true;
        Side = CombatSide.None;
        SideTurnPhase = CombatSideTurnPhase.None;

        StartSideTurn();
    }

    public void EndCurrentSideTurnAndStartNextOne()
    {
        EndSideTurn();

        if (Over)
        {
            return;
        }

        StartSideTurn();
    }

    public void PlayCardAt(int index)
    {
        CurrentSide.PlayCardAt(index);

        if (CheckWinCondition())
        {
            return;
        }

        AutoEndCurrentTurnIfNoMovesLeft();
    }

    public CharacterCombatState? GetCharacter(CombatSide side, CombatPosition position)
    {
        CombatSideInstance combatSide = side switch
        {
            CombatSide.Left => _leftSide,
            CombatSide.Right => _rightSide,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        return position switch
        {
            CombatPosition.Front => combatSide.Front,
            CombatPosition.Back => combatSide.Back,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    public IEnumerable<CharacterCombatState> GetAllCharacters(CombatSide side)
    {
        CombatSideInstance combatSide = side switch
        {
            CombatSide.Left => _leftSide,
            CombatSide.Right => _rightSide,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        return combatSide.Both;
    }

    void StartSideTurn()
    {
        AssertOngoing();
        AssertNotOver();
        AssertSideTurnNotStartedYet();

        Side = Options.StartingSide;
        SideTurnPhase = CombatSideTurnPhase.StartOfTurn;

        StartOfTurnResolver.Resolve(this);

        if (CheckWinCondition())
        {
            return;
        }

        if (AutoEndCurrentTurnIfNoMovesLeft())
        {
            return;
        }

        SideTurnPhase = CombatSideTurnPhase.Draw;

        CurrentSide.DrawUntil(Options.HandSize);

        SideTurnPhase = CombatSideTurnPhase.Play;

        CurrentSide.StartTurn();
    }

    void EndSideTurn()
    {
        AssertSideTurnStarted();

        SideTurnPhase = CombatSideTurnPhase.EndOfTurn;

        EndOfTurnResolver.Resolve(this);

        if (CheckWinCondition())
        {
            return;
        }

        if (AutoEndCurrentTurnIfNoMovesLeft())
        {
            return;
        }

        SideTurnPhase = CombatSideTurnPhase.None;
        Side = NextSide();

        if (Side == CombatSide.None)
        {
            Turn++;
        }
    }

    bool AutoEndCurrentTurnIfNoMovesLeft()
    {
        if (!CurrentSide.NoMovesLeft)
        {
            return false;
        }

        EndSideTurn();
        StartSideTurn();
        return true;
    }

    CombatSide NextSide()
    {
        if (Side == CombatSide.None)
        {
            return Options.StartingSide;
        }

        return Side == Options.StartingSide ? Side.OtherSide() : CombatSide.None;
    }

    bool CheckWinCondition()
    {
        _leftSide.RemoveDeadCreatures();
        _rightSide.RemoveDeadCreatures();

        bool leftLost = _leftSide.Lost;
        bool rightLost = _rightSide.Lost;

        if (leftLost && rightLost)
        {
            EndCombat(CombatSide.None);
            return true;
        }

        if (leftLost)
        {
            EndCombat(CombatSide.Right);
            return true;
        }

        if (rightLost)
        {
            EndCombat(CombatSide.Left);
            return true;
        }

        return false;
    }

    void EndCombat(CombatSide winner)
    {
        Ongoing = false;
        Over = true;
        Winner = winner;
    }

    class CombatSideInstance
    {
        readonly List<ActionCardInstance> _deck;
        readonly List<ActionCardInstance> _hand;

        public CombatSideInstance(CombatInstance combat, CombatSide side, CharacterCombatState front, CharacterCombatState? back = null)
        {
            Combat = combat;
            Side = side;
            Front = front;
            Back = back;

            Ap = 0;

            _hand = new List<ActionCardInstance>();

            _deck = new List<ActionCardInstance>();
            _deck.AddRange(front.Character.Deck.Select(c => new ActionCardInstance(c, front)));
            if (back != null)
            {
                _deck.AddRange(back.Character.Deck.Select(c => new ActionCardInstance(c, back)));
            }

            ShuffleDeck();
        }

        public CombatInstance Combat { get; }
        public CombatSide Side { get; }

        public CharacterCombatState Front { get; private set; }
        public CharacterCombatState? Back { get; private set; }
        public IEnumerable<CharacterCombatState> Both => new[] { Front, Back }.Where(c => c != null).Select(c => c!);

        public int Ap { get; private set; }

        public IReadOnlyList<ActionCardInstance> Hand => _hand;
        public IReadOnlyList<ActionCardInstance> Deck => _deck;

        public bool Lost => Front.IsDead && (Back == null || Back.IsDead);
        public bool NoMovesLeft => _hand.All(c => c.ApCost > Ap);

        public void StartTurn()
        {
            Ap = Math.Min(Combat.Options.StartingAp + Combat.Turn, Combat.Options.MaxAp);
        }

        public void PlayCardAt(int index)
        {
            ActionCardInstance? card = _hand.ElementAtOrDefault(index);
            if (card == null)
            {
                throw new IndexOutOfRangeException();
            }

            ConsumeAp(card.ApCost);

            _hand.RemoveAt(index);

            int randomPosition = Random.Shared.Next(0, Deck.Count + 1);
            _deck.Insert(randomPosition, card);

            card.Resolve();
        }

        public bool DrawCard()
        {
            ActionCardInstance? card = _deck.FirstOrDefault();
            if (card == null)
            {
                return false;
            }

            _hand.Add(card);
            _deck.RemoveAt(0);

            return true;
        }

        public void DrawUntil(int nCards)
        {
            while (_hand.Count < nCards)
            {
                if (!DrawCard())
                {
                    break;
                }
            }
        }

        public void Swap()
        {
            if (Back == null)
            {
                return;
            }

            (Front, Back) = (Back, Front);
        }

        public void RemoveDeadCreatures()
        {
            if (Back != null && Back.IsDead)
            {
                Back = null;
            }

            if (Front.IsDead)
            {
                if (Back == null)
                {
                    // Side has lost
                    return;
                }

                Front = Back;
                Back = null;
            }
        }

        void ShuffleDeck()
        {
            Random.Shared.Shuffle(_deck);
        }

        void ConsumeAp(int ap)
        {
            if (ap <= 0)
            {
                return;
            }

            Ap -= ap;
        }
    }

    #region Assertions

    void AssertNotStarted()
    {
        if (Ongoing)
        {
            throw new Exception("Already started");
        }
    }

    void AssertOngoing()
    {
        if (!Ongoing)
        {
            throw new Exception("Combat not started yet");
        }
    }

    void AssertSideTurnStarted()
    {
        if (SideTurnPhase == CombatSideTurnPhase.None)
        {
            throw new Exception("Side turn not started yet");
        }
    }

    void AssertSideTurnNotStartedYet()
    {
        if (SideTurnPhase != CombatSideTurnPhase.None)
        {
            throw new Exception("Side turn already started");
        }
    }

    void AssertNotOver()
    {
        if (Over)
        {
            throw new Exception("Combat already over");
        }
    }

    #endregion
}
