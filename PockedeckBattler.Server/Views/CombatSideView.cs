using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class CombatSideView
{
    public CombatSideView(string playerName, CombatSide side, int ap, CharacterCombatView frontCharacter, CharacterCombatView? backCharacter = null)
    {
        PlayerName = playerName;
        Side = side;
        Ap = ap;
        FrontCharacter = frontCharacter;
        BackCharacter = backCharacter;
    }

    [Required]
    public string PlayerName { get; }

    public CombatSide Side { get; }
    public int Ap { get; }
    public int HandSize { get; init; }
    public int DeckSize { get; init; }

    [Required]
    public CharacterCombatView FrontCharacter { get; }

    public CharacterCombatView? BackCharacter { get; }
}

public static class CombatSideViewMappingExtensions
{
    public static CombatSideView View(this CombatInstance.CombatSideInstance side, string playerName)
    {
        return new CombatSideView(playerName, side.Side, side.Ap, side.Front.View(), side.Back?.View())
        {
            HandSize = side.Hand.Count,
            DeckSize = side.Deck.Count
        };
    }
}
