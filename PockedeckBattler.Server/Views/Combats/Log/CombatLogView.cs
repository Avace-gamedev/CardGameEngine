using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CombatLogView
{
    public CombatLogView(params CombatLogTurnView[] turns)
    {
        Turns = turns;
    }

    public CombatLogTurnView[] Turns { get; }
}

public static class CombatLogViewMappingExtensions
{
    public static CombatLogView View(this CombatLog log)
    {
        List<CombatLogTurnView> turns = new();

        int? currentTurn = null;
        List<CombatLogEntry> turnEntries = new();
        foreach (CombatLogEntry entry in log.Entries)
        {
            if (entry is TurnPhaseChangedLogEntry turnEntry && turnEntry.Turn != currentTurn)
            {
                if (currentTurn.HasValue)
                {
                    turns.Add(turnEntries.View(currentTurn.Value));
                }

                currentTurn = turnEntry.Turn;
                turnEntries = new List<CombatLogEntry> { turnEntry };
            }
            else
            {
                turnEntries.Add(entry);
            }
        }

        if (currentTurn.HasValue)
        {
            turns.Add(turnEntries.View(currentTurn.Value));
        }

        return new CombatLogView(turns.ToArray());
    }
}
