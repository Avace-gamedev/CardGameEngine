using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CombatEndedLogEntryView : CombatLogEntryView
{
    public CombatEndedLogEntryView(CombatSide winner)
    {
        Winner = winner;
    }

    public CombatSide Winner { get; }
}

public static class CombatEndedLogEntryViewMappingExtensions
{
    public static CombatEndedLogEntryView View(this CombatEndedLogEntry entry)
    {
        return new CombatEndedLogEntryView(entry.Winner);
    }
}
