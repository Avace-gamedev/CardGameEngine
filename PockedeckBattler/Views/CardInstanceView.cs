using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Views;

public class CardInstanceView
{
    public CardInstanceView(ActionCard card, string character)
    {
        Card = card;
        Character = character;
    }

    ActionCard Card { get; }
    string Character { get; }
}

public static class CardInstanceViewMappingExtensions
{
    public static CardInstanceView View(this ActionCardInstance card)
    {
        return new CardInstanceView(card.Card, card.Character.Character.Identity.Name);
    }
}
