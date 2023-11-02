using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Views;

public class CombatView : BaseCombatView
{
    public CombatView(Guid id, CombatSideView leftSide, CombatSideView rightSide, int turn, CombatSide currentSide, CombatSideTurnPhase currentPhase) : base(
        turn,
        currentSide,
        currentPhase
    )
    {
        Id = id;
        LeftSide = leftSide;
        RightSide = rightSide;
    }

    [Required]
    public Guid Id { get; }

    [Required]
    public CombatSideView LeftSide { get; }

    [Required]
    public CombatSideView RightSide { get; }
}

public static class CombatViewMappingExtensions
{
    public static CombatView View(this Combat store)
    {
        return new CombatView(
            store.Id,
            store.Instance.LeftSide.View(store.LeftPlayerName),
            store.Instance.RightSide.View(store.RightPlayerName),
            store.Instance.Turn,
            store.Instance.Side,
            store.Instance.Phase
        )
        {
            Ongoing = store.Instance.Ongoing,
            Over = store.Instance.Over
        };
    }
}
