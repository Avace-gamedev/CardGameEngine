using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CombatLogPhaseView
{
    public CombatLogPhaseView(CombatSide side, CombatSideTurnPhase phase, params CombatLogEntryView[] entries)
    {
        Side = side;
        Phase = phase;
        Entries = entries;
    }

    public CombatSide Side { get; }
    public CombatSideTurnPhase Phase { get; }
    public CombatLogEntryView[] Entries { get; }
}

public static class CombatLogPhaseViewMappingExtensions
{
    public static CombatLogPhaseView View(this CombatLog.TurnPhase phase)
    {
        return new CombatLogPhaseView(phase.Side, phase.Phase, phase.Entries.Select(e => e.View()).ToArray());
    }
}
