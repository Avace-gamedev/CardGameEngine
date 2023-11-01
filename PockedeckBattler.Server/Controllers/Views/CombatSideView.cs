using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Controllers.Views;

public class CombatSideView : BaseCombatSideView
{
    public CombatSideView(string playerName, CombatSide side, int handSize, CharacterCombatView frontCharacter, CharacterCombatView? backCharacter = null) : base(
        side,
        frontCharacter,
        backCharacter
    )
    {
        PlayerName = playerName;
        HandSize = handSize;
    }

    [Required]
    string PlayerName { get; }

    int HandSize { get; }
}

public static class CombatSideViewMappingExtensions
{
    public static CombatSideView View(this CombatInstance.CombatSideInstance side, string playerName)
    {
        return new CombatSideView(playerName, side.Side, side.Hand.Count, side.Front.View(), side.Back?.View());
    }
}
