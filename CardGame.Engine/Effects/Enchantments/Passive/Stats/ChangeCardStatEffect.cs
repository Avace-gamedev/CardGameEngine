using CardGame.Engine.Combats.Modifiers;

namespace CardGame.Engine.Effects.Enchantments.Passive.Stats;

public class ChangeCardStatEffect : PassiveEffect
{
    public ChangeCardStatEffect(CardStatEffectType type, int amount, int duration) : base(duration)
    {
        Type = type;
        Amount = amount;
    }

    public CardStatEffectType Type { get; }
    public int Amount { get; }

    public CardStatsModifier GetStatsModifier()
    {
        return Type switch
        {
            CardStatEffectType.IncreaseApCost => new CardStatsModifier { ApCostAdditiveModifier = Amount },
            CardStatEffectType.ReduceApCost => new CardStatsModifier { ApCostAdditiveModifier = -Amount },
            CardStatEffectType.IncreaseDamage => new CardStatsModifier { ImmediateDamageAdditiveModifier = Amount },
            CardStatEffectType.ReduceDamage => new CardStatsModifier { ImmediateDamageAdditiveModifier = -Amount },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum CardStatEffectType
{
    IncreaseApCost,
    ReduceApCost,
    IncreaseDamage,
    ReduceDamage
}
