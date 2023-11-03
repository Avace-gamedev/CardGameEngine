using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Passive.Stats;

public class PassiveStatsModifier : PassiveEffect
{
    public PassiveStatsModifier(StatEffect effect, int amount, int duration) : base(duration)
    {
        Effect = effect;
        Amount = amount;
    }

    public StatEffect Effect { get; }
    public int Amount { get; }

    public StatsModifier GetStatsModifier()
    {
        return Effect switch
        {
            StatEffect.IncreaseApCost => new StatsModifier { ApCostAdditiveModifier = Amount },
            StatEffect.ReduceApCost => new StatsModifier { ApCostAdditiveModifier = -Amount },
            StatEffect.IncreaseDamage => new StatsModifier { DamageAdditiveModifier = Amount },
            StatEffect.ReduceDamage => new StatsModifier { DamageAdditiveModifier = -Amount },
            StatEffect.IncreaseResistance => new StatsModifier { ResistanceAdditiveModifier = Amount },
            StatEffect.ReduceResistance => new StatsModifier { ResistanceAdditiveModifier = -Amount },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
