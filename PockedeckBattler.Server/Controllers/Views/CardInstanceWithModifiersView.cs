using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Controllers.Views.Effects.Active;

namespace PockedeckBattler.Server.Controllers.Views;

public class CardInstanceWithModifiersView : CardInstanceView
{
    public CardInstanceWithModifiersView(ActionCardView cardWithModifiers, ActionCardView baseCard, string character) : base(cardWithModifiers, character)
    {
        BaseCard = baseCard;
    }

    [Required]
    public ActionCardView BaseCard { get; }
}

public static class CardInstanceWithModifiersViewMappingExtensions
{
    public static CardInstanceWithModifiersView ViewWithModifiers(this ActionCardInstance card)
    {
        ActionCardView cardWithModifiers = new(
            card.Card.Name,
            card.Card.Description,
            card.ApCost,
            card.Card.Target,
            card.Card.MainEffect.View(),
            card.Card.AdditionalEffects.Select(c => c.View()).ToArray()
        );

        return new CardInstanceWithModifiersView(cardWithModifiers, card.Card.View(), card.Character.Character.Identity.Name);
    }
}
