using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Views;

public class CardInstanceWithModifiersView : CardInstanceView
{
    public CardInstanceWithModifiersView(ActionCard cardWithModifiers, ActionCard baseCard, string character) : base(cardWithModifiers, character)
    {
        BaseCard = baseCard;
    }

    public ActionCard BaseCard { get; }
}

public static class CardInstanceWithModifiersViewMappingExtensions
{
    public static CardInstanceWithModifiersView ViewWithModifiers(this ActionCardInstance card)
    {
        ActionCard cardWithModifiers = new(
            card.Card.Name,
            card.Card.Description,
            card.ApCost,
            card.Card.Target,
            card.Card.MainEffect,
            card.Card.AdditionalEffects.ToArray()
        );

        return new CardInstanceWithModifiersView(cardWithModifiers, card.Card, card.Character.Character.Identity.Name);
    }
}
