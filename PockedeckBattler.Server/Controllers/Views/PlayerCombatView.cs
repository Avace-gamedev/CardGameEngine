using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores;

namespace PockedeckBattler.Server.Controllers.Views;

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
    public static PlayerCombatView PlayerView(this StoredCombat combat, CombatSide side)
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
            combat.Combat.GetSide(side).PlayerView(sidePlayerName),
            combat.Combat.GetSide(side.OtherSide()).View(otherSidePlayerName),
            combat.Combat.Turn,
            combat.Combat.Side,
            combat.Combat.Phase
        )
        {
            Ongoing = combat.Combat.Ongoing,
            Over = combat.Combat.Over
        };
    }
}
