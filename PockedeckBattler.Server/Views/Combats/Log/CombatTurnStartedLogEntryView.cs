using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CombatTurnStartedLogEntryView : CombatLogEntryView
{
    public CombatTurnStartedLogEntryView(int turn)
    {
        Turn = turn;
    }

    public int Turn { get; }
}

public static class CombatTurnStartedLogEntryViewMappingExtensions
{
    public static CombatTurnStartedLogEntryView View(this CombatTurnStartedLogEntry entry)
    {
        return new CombatTurnStartedLogEntryView(entry.Turn);
    }
}
