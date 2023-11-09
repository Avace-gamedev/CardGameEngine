using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class TurnStartedLogEntryView : CombatLogEntryView
{
    public TurnStartedLogEntryView(int turn)
    {
        Turn = turn;
    }

    public int Turn { get; }
}

public static class TurnStartedLogEntryViewMappingExtensions
{
    public static TurnStartedLogEntryView View(this TurnStartedLogEntry entry)
    {
        return new TurnStartedLogEntryView(entry.Turn);
    }
}
