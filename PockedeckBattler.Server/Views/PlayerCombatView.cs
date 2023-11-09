using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats.Abstractions;
using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Views.Combats.Log;

namespace PockedeckBattler.Server.Views;

public class PlayerCombatView : BaseCombatView
{
    public PlayerCombatView(
        Guid id,
        PlayerSideView player,
        CombatSideView opponent,
        int turn,
        int maxAp,
        CombatSide currentSide,
        CombatSideTurnPhase currentPhase
    ) : base(turn, maxAp, currentSide, currentPhase)
    {
        Id = id;
        Player = player;
        Opponent = opponent;
    }

    [Required]
    public Guid Id { get; }

    [Required]
    public PlayerSideView Player { get; }

    public bool PlayerIsAi { get; init; }

    [Required]
    public CombatSideView Opponent { get; }

    public bool OpponentIsAi { get; init; }
}

public static class PlayerCombatViewMappingExtensions
{
    public static PlayerCombatView PlayerView(this CombatInstanceWithMetadata combat, CombatSide side)
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
            combat.Instance.State.GetSide(side).PlayerView(sidePlayerName),
            combat.Instance.State.GetSide(side.OtherSide()).View(otherSidePlayerName),
            combat.Instance.State.Turn,
            combat.Instance.State.MaxAp,
            combat.Instance.State.Side,
            combat.Instance.State.Phase
        )
        {
            PlayerIsAi = side switch
            {
                CombatSide.Left => combat.Instance.LeftAi != null, CombatSide.Right => combat.Instance.RightAi != null
            },
            OpponentIsAi = side switch
            {
                CombatSide.Left => combat.Instance.RightAi != null, CombatSide.Right => combat.Instance.LeftAi != null
            },
            Ongoing = combat.Instance.State.Ongoing,
            Over = combat.Instance.State.Over,
            Winner = combat.Instance.State.Winner,
            LeftPlayerName = combat.LeftPlayerName,
            RightPlayerName = combat.RightPlayerName,
            Log = combat.Instance.State.Log.View()
        };
    }
}
