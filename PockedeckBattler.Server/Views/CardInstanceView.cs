using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class CardInstanceView
{
    public CardInstanceView(ActionCardView card, string character)
    {
        Card = card;
        Character = character;
    }

    [Required]
    public ActionCardView Card { get; }

    public string Character { get; }
}

public static class CardInstanceViewMappingExtensions
{
    public static CardInstanceView View(this ActionCardInstance card)
    {
        return new CardInstanceView(card.Card.View(), card.Character.Character.Identity.Name);
    }
}
