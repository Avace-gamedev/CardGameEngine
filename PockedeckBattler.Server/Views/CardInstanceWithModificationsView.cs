using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class CardInstanceWithModificationsView : CardInstanceView
{
    public CardInstanceWithModificationsView(ActionCardView cardWithModifiers, ActionCardView baseCard, string character) : base(cardWithModifiers, character)
    {
        BaseCard = baseCard;
    }

    [Required]
    public ActionCardView BaseCard { get; }
}

public static class CardInstanceWithModifiersViewMappingExtensions
{
    public static CardInstanceWithModificationsView ViewWithModifiers(this ActionCardInstance card)
    {
        ActionCard withModifications = card.GetCardWithModifications();
        return new CardInstanceWithModificationsView(withModifications.View(), card.Card.View(), card.Character.Character.Identity.Name);
    }
}
