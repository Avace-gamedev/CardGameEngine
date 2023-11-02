using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Views;

public class PlayerCombatView : BaseCombatView
{
    public PlayerCombatView(Guid id, PlayerSideView player, CombatSideView combat, int turn, CombatSide currentSide, CombatSideTurnPhase currentPhase) : base(
        turn,
        currentSide,
        currentPhase
    )
    {
        Id = id;
        Player = player;
        Combat = combat;
    }

    [Required]
    public Guid Id { get; }

    [Required]
    public PlayerSideView Player { get; }

    [Required]
    public CombatSideView Combat { get; }
}

public static class PlayerCombatViewMappingExtensions
{
    public static PlayerCombatView PlayerView(this Combat combat, CombatSide side)
    {
        string sidePlayerName = side switch
        {
            CombatSide.Left => combat.LeftPlayerName,
            CombatSide.Right => combat.RightPlayerName,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        string otherSidePlayerName = side switch
        {
            CombatSide.Left => combat.RightPlayerName,
            CombatSide.Right => combat.LeftPlayerName,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        return new PlayerCombatView(
            combat.Id,
            combat.Instance.GetSide(side).PlayerView(sidePlayerName),
            combat.Instance.GetSide(side.OtherSide()).View(otherSidePlayerName),
            combat.Instance.Turn,
            combat.Instance.Side,
            combat.Instance.Phase
        )
        {
            Ongoing = combat.Instance.Ongoing,
            Over = combat.Instance.Over
        };
    }
}
