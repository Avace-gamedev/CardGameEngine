using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CombatLogTurnView
{
    public CombatLogTurnView(int turn, params CombatLogPhaseView[] phases)
    {
        Turn = turn;
        Phases = phases;
    }

    public int Turn { get; }
    public CombatLogPhaseView[] Phases { get; }
}

public static class CombatLogTurnViewMappingExtensions
{
    public static CombatLogTurnView View(this IEnumerable<CombatLogEntry> entries, int turn)
    {
        List<CombatLogPhaseView> phases = new();

        TurnPhaseChangedLogEntry? currentPhase = null;
        List<CombatLogEntry> phaseEntries = new();
        foreach (CombatLogEntry entry in entries)
        {
            if (entry is TurnPhaseChangedLogEntry phaseChange)
            {
                if (currentPhase != null)
                {
                    phases.Add(phaseEntries.View(currentPhase.Side, currentPhase.Phase));
                }

                currentPhase = phaseChange;
                phaseEntries = new List<CombatLogEntry>();
            }
            else
            {
                phaseEntries.Add(entry);
            }
        }

        if (currentPhase != null)
        {
            phases.Add(phaseEntries.View(currentPhase.Side, currentPhase.Phase));
        }

        return new CombatLogTurnView(turn, phases.ToArray());
    }
}
