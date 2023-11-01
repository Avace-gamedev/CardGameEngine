using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores;

namespace PockedeckBattler.Server.Controllers.Views;

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

    [Required]
    public CombatSideView LeftSide { get; }

    [Required]
    public CombatSideView RightSide { get; }
}

public static class CombatViewMappingExtensions
{
    public static CombatView View(this StoredCombat store)
    {
        return new CombatView(
            store.Combat.LeftSide.View(store.LeftPlayerName),
            store.Combat.RightSide.View(store.RightPlayerName),
            store.Combat.Turn,
            store.Combat.Side,
            store.Combat.Phase
        )
        {
            Ongoing = store.Combat.Ongoing,
            Over = store.Combat.Over
        };
    }
}
