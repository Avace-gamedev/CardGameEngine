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
        return new CombatLogView(log.Turns.Select(t => t.View()).ToArray());
    }
}
