using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Logs;

public class TurnPhaseChangedLogEntry : CombatLogEntry
{
    public TurnPhaseChangedLogEntry(int turn, CombatSide side, CombatSideTurnPhase phase)
    {
        Turn = turn;
        Side = side;
        Phase = phase;
    }

    public int Turn { get; }
    public CombatSide Side { get; }
    public CombatSideTurnPhase Phase { get; }
}
