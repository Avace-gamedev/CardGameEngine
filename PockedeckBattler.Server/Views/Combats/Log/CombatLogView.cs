using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CombatLogView
{
    public CombatLogView(CombatLogEntryView[] entries)
    {
        Entries = entries;
    }

    public CombatLogEntryView[] Entries { get; }
}

public static class CombatLogViewMappingExtensions
{
    public static CombatLogView View(this CombatLog log)
    {
        return new CombatLogView(log.Entries.Select(e => e.View()).ToArray());
    }
}
