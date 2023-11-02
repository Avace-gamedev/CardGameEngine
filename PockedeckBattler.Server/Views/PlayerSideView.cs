using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class PlayerSideView : BaseCombatSideView
{
    public PlayerSideView(
        string playerName,
        CombatSide side,
        CardInstanceWithModifiersView[] hand,
        CharacterCombatView frontCharacter,
        CharacterCombatView? backCharacter = null
    ) : base(side, frontCharacter, backCharacter)
    {
        PlayerName = playerName;
        Hand = hand;
    }

    [Required]
    public string PlayerName { get; }

    [Required]
    public CardInstanceWithModifiersView[] Hand { get; }
}

public static class PlayerSideViewMappingExtensions
{
    public static PlayerSideView PlayerView(this CombatInstance.CombatSideInstance side, string playerName)
    {
        return new PlayerSideView(playerName, side.Side, side.Hand.Select(c => c.ViewWithModifiers()).ToArray(), side.Front.View(), side.Back?.View());
    }
}
