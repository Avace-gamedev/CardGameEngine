using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Effects.Active;

namespace CardGame.Engine.Combats.Modifiers;

public class CardStatsModifier
{
    public static CardStatsModifier None => new() { ApCostAdditiveModifier = 0, ImmediateDamageAdditiveModifier = 0 };

    public int ApCostAdditiveModifier { get; init; }
    public int ImmediateDamageAdditiveModifier { get; init; }

    public ActionCard Apply(ActionCard card)
    {
        return new ActionCard(
            card.Name,
            card.Description,
            card.ApCost + ApCostAdditiveModifier,
            card.Target,
            Apply(card.MainEffect),
            card.AdditionalEffects.Select(Apply).ToArray()
        );
    }

    ActiveEffect Apply(ActiveEffect effect)
    {
        switch (effect)
        {
            case DamageEffect damageEffect:
                return damageEffect.ChangeDamageAmount(damageEffect.Amount + ImmediateDamageAdditiveModifier);
            case AddEnchantmentEffect addPassiveEffect:
            case HealEffect healEffect:
            case RandomEffect randomEffect:
            case ShieldEffect shieldEffect:
                return effect;
            default:
                throw new ArgumentOutOfRangeException(nameof(effect));

        }
    }

    public static CardStatsModifier Combine(CardStatsModifier modifier1, CardStatsModifier modifier2)
    {
        return new CardStatsModifier
        {
            ApCostAdditiveModifier = modifier1.ApCostAdditiveModifier + modifier2.ApCostAdditiveModifier,
            ImmediateDamageAdditiveModifier = modifier1.ImmediateDamageAdditiveModifier + modifier2.ImmediateDamageAdditiveModifier
        };
    }
}
