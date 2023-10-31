using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Exceptions;
using CardGame.Engine.Combats.Resolve;
using CardGame.Engine.Extensions;

namespace CardGame.Engine.Combats;

public class CombatInstance
{
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

        LeftSide = new CombatSideInstance(this, CombatSide.Left, frontLeft, backLeft);
        RightSide = new CombatSideInstance(this, CombatSide.Right, frontRight, backRight);

        Ongoing = false;
        Over = false;

        Turn = 0;
        Side = CombatSide.None;
        Phase = CombatSideTurnPhase.None;
    }

    public CombatSideInstance LeftSide { get; }
    public CombatSideInstance RightSide { get; }
    public CombatSideInstance CurrentSide => GetSide(Side);
    public CombatSideInstance OtherSide => GetSide(Side.OtherSide());

    public CombatOptions Options { get; }

    public bool Ongoing { get; private set; }
    public bool Over { get; private set; }
    public int Turn { get; private set; }
    public CombatSide Side { get; private set; }
    public CombatSideTurnPhase Phase { get; private set; }
    public CombatSide Winner { get; private set; }

    public void Start()
    {
        AssertNotStarted();
        AssertNotOver();

        Turn = 1;
        Ongoing = true;
        Side = Options.StartingSide;
        Phase = CombatSideTurnPhase.None;

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

    public void PlayCardAt(CombatSide side, int index)
    {
        AssertOngoing();
        AssertSideCanPlay(side);

        CurrentSide.PlayCardAt(index);

        CheckWinCondition();
    }

    public CombatSideInstance GetSide(CombatSide side)
    {
        return side switch
        {
            CombatSide.Left => LeftSide,
            CombatSide.Right => RightSide,
            _ => throw new ArgumentOutOfRangeException(nameof(Side), Side, null)
        };
    }

    public CharacterCombatState? GetCharacter(CombatSide side, CombatPosition position)
    {
        CombatSideInstance combatSide = side switch
        {
            CombatSide.Left => LeftSide,
            CombatSide.Right => RightSide,
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
            CombatSide.Left => LeftSide,
            CombatSide.Right => RightSide,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        return combatSide.Both;
    }

    void StartSideTurn()
    {
        AssertOngoing();
        AssertNotOver();
        AssertSideTurnNotStartedYet();

        Phase = CombatSideTurnPhase.StartOfTurn;

        StartOfTurnResolver.Resolve(this);

        if (CheckWinCondition())
        {
            return;
        }

        Phase = CombatSideTurnPhase.Draw;

        CurrentSide.DrawUntil(Options.HandSize);

        Phase = CombatSideTurnPhase.Play;

        CurrentSide.StartTurn();
    }

    void EndSideTurn()
    {
        AssertSideTurnStarted();

        Phase = CombatSideTurnPhase.EndOfTurn;

        EndOfTurnResolver.Resolve(this);

        if (CheckWinCondition())
        {
            return;
        }

        CurrentSide.EndTurn();

        Phase = CombatSideTurnPhase.None;
        Side = NextSide();

        if (Side == CombatSide.None)
        {
            Turn++;
            Side = Options.StartingSide;
        }
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
        LeftSide.RemoveDeadCreatures();
        RightSide.RemoveDeadCreatures();

        bool leftLost = LeftSide.Lost;
        bool rightLost = RightSide.Lost;

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

    public class CombatSideInstance
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

        internal void StartTurn()
        {
            Ap = Math.Min(Combat.Options.StartingAp + Combat.Turn - 1, Combat.Options.MaxAp);
        }

        internal void EndTurn()
        {
            Ap = Math.Min(Combat.Options.StartingAp + Combat.Turn - 1, Combat.Options.MaxAp);
        }

        internal void PlayCardAt(int index)
        {
            ActionCardInstance? card = _hand.ElementAtOrDefault(index);
            if (card == null)
            {
                throw new InvalidMoveException($"Could not find card at index {index}");
            }

            ConsumeAp(card.ApCost);

            _hand.RemoveAt(index);

            int randomPosition = Random.Shared.Next(0, Deck.Count + 1);
            _deck.Insert(randomPosition, card);

            card.Resolve();
        }

        internal bool DrawCard()
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

        internal void DrawUntil(int nCards)
        {
            while (_hand.Count < nCards)
            {
                if (!DrawCard())
                {
                    break;
                }
            }
        }

        internal void Swap()
        {
            if (Back == null)
            {
                return;
            }

            (Front, Back) = (Back, Front);
        }

        internal void RemoveDeadCreatures()
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

            if (ap > Ap)
            {
                throw new InvalidMoveException("Not enough Ap");
            }

            Ap -= ap;
        }
    }

    #region Assertions

    void AssertNotStarted()
    {
        if (Ongoing)
        {
            throw new InvalidMoveException("Already started");
        }
    }

    void AssertOngoing()
    {
        if (!Ongoing)
        {
            throw new InvalidMoveException("Combat not started yet");
        }
    }

    void AssertSideCanPlay(CombatSide side)
    {
        if (Side != side || Phase != CombatSideTurnPhase.Play)
        {
            throw new InvalidMoveException("Not your turn");
        }
    }

    void AssertSideTurnStarted()
    {
        if (Phase == CombatSideTurnPhase.None)
        {
            throw new InvalidMoveException("Side turn not started yet");
        }
    }

    void AssertSideTurnNotStartedYet()
    {
        if (Phase != CombatSideTurnPhase.None)
        {
            throw new InvalidMoveException("Side turn already started");
        }
    }

    void AssertNotOver()
    {
        if (Over)
        {
            throw new InvalidMoveException("Combat already over");
        }
    }

    #endregion
}
