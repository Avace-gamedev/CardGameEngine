using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Views;

public class PlayerCombatView : BaseCombatView
{
    public PlayerCombatView(Guid id, PlayerSideView player, CombatSideView opponent, int turn, CombatSide currentSide, CombatSideTurnPhase currentPhase) : base(
        turn,
        currentSide,
        currentPhase
    )
    {
        Id = id;
        Player = player;
        Opponent = opponent;
    }

    [Required]
    public Guid Id { get; }

    [Required]
    public PlayerSideView Player { get; }

    [Required]
    public CombatSideView Opponent { get; }
}

public static class PlayerCombatViewMappingExtensions
{
    public static PlayerCombatView PlayerView(this CombatWithMetadata combatWithMetadata, CombatSide side)
    {
        string sidePlayerName = side switch
        {
            CombatSide.Left => combatWithMetadata.LeftPlayerName,
            CombatSide.Right => combatWithMetadata.RightPlayerName,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        string otherSidePlayerName = side switch
        {
            CombatSide.Left => combatWithMetadata.RightPlayerName,
            CombatSide.Right => combatWithMetadata.LeftPlayerName,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };

        return new PlayerCombatView(
            combatWithMetadata.Id,
            combatWithMetadata.Instance.GetSide(side).PlayerView(sidePlayerName),
            combatWithMetadata.Instance.GetSide(side.OtherSide()).View(otherSidePlayerName),
            combatWithMetadata.Instance.Turn,
            combatWithMetadata.Instance.Side,
            combatWithMetadata.Instance.Phase
        )
        {
            Ongoing = combatWithMetadata.Instance.Ongoing,
            Over = combatWithMetadata.Instance.Over
        };
    }
}
