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
    public static CombatLogTurnView View(this CombatLog.CombatTurn turn)
    {
        return new CombatLogTurnView(turn.Turn, turn.Phases.Select(p => p.View()).ToArray());
    }
}
