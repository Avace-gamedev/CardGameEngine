using CardGame.Engine.Characters;

namespace CardGame.Engine.Combats.State;

public class CombatState
{
    public CombatState(CombatSideState leftSide, CombatSideState rightSide)
    {
        Ongoing = false;
        Over = false;

        Turn = 0;
        MaxAp = 0;
        Side = CombatSide.None;
        Phase = CombatSideTurnPhase.None;
        Winner = CombatSide.None;

        LeftSide = leftSide;
        RightSide = rightSide;
    }

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

    public event EventHandler<TurnStartedEventArgs>? TurnStarted;
    public event EventHandler<PhaseStartedEventArgs>? PhaseStarted;
    public event EventHandler<CombatEndedEventArgs>? Ended;

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
        Ongoing = true;
        Over = false;

        Turn = turn;
        MaxAp = maxAp;

        Side = CombatSide.None;

        TurnStarted?.Invoke(this, new TurnStartedEventArgs(Turn));
    }

    internal void StartPhaseOfSide(CombatSide side, CombatSideTurnPhase phase)
    {
        Side = side;
        Phase = phase;

        if (side != CombatSide.None)
        {
            PhaseStarted?.Invoke(this, new PhaseStartedEventArgs(Turn, Side, Phase));
        }
    }

    internal void EndCombat(CombatSide winner)
    {
        Ongoing = false;
        Over = true;

        Side = CombatSide.None;
        Phase = CombatSideTurnPhase.None;

        Winner = winner;

        Ended?.Invoke(this, new CombatEndedEventArgs(Winner));
    }

    public static CombatState Create(IReadOnlyList<Character> leftCharacters, IReadOnlyList<Character> rightCharacters)
    {
        CombatSideState leftSide = CombatSideState.Create(CombatSide.Left, leftCharacters);
        CombatSideState rightSide = CombatSideState.Create(CombatSide.Right, leftCharacters);

        return new CombatState(leftSide, rightSide);
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

public class TurnStartedEventArgs
{
    public TurnStartedEventArgs(int turn)
    {
        Turn = turn;
    }

    int Turn { get; }
}

public class PhaseStartedEventArgs
{
    public PhaseStartedEventArgs(int turn, CombatSide side, CombatSideTurnPhase phase)
    {
        Turn = turn;
        Side = side;
        Phase = phase;
    }

    public int Turn { get; }
    public CombatSide Side { get; }
    public CombatSideTurnPhase Phase { get; }
}
