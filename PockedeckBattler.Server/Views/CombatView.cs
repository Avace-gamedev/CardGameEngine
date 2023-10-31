using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class CombatView : BaseCombatView
{
    public CombatView(CombatSideView leftSide, CombatSideView rightSide, int turn, CombatSide currentSide, CombatSideTurnPhase currentPhase) : base(
        turn,
        currentSide,
        currentPhase
    )
    {
        LeftSide = leftSide;
        RightSide = rightSide;
    }

    public CombatSideView LeftSide { get; }
    public CombatSideView RightSide { get; }
}

public static class CombatViewMappingExtensions
{
    public static CombatView View(this CombatInstance combat)
    {
        return new CombatView(combat.LeftSide.View(), combat.RightSide.View(), combat.Turn, combat.Side, combat.Phase)
        {
            Ongoing = combat.Ongoing,
            Over = combat.Over
        };
    }
}
