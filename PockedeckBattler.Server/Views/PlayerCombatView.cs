using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class PlayerCombatView : BaseCombatView
{
    public PlayerCombatView(PlayerSideView player, CombatSideView combat, int turn, CombatSide currentSide, CombatSideTurnPhase currentPhase) : base(
        turn,
        currentSide,
        currentPhase
    )
    {
        Player = player;
        Combat = combat;
    }

    public PlayerSideView Player { get; }
    public CombatSideView Combat { get; }
}

public static class PlayerCombatViewMappingExtensions
{
    public static PlayerCombatView PlayerView(this CombatInstance combat, CombatSide side)
    {
        return new PlayerCombatView(combat.GetSide(side).PlayerView(), combat.GetSide(side.OtherSide()).View(), combat.Turn, combat.Side, combat.Phase)
        {
            Ongoing = combat.Ongoing,
            Over = combat.Over
        };
    }
}
