using CardGame.Engine.Combats.Modifiers;

namespace CardGame.Engine.Effects.Passive.Stats;

public class CharacterStatEffect : PassiveEffect
{
    public CharacterStatEffect(CharacterStatEffectType type, int amount, int duration) : base(duration)
    {
        Type = type;
        Amount = amount;
    }

    public CharacterStatEffectType Type { get; }
    public int Amount { get; }

    public CharacterStatsModifier GetStatsModifier()
    {
        return Type switch
        {
            CharacterStatEffectType.IncreaseAllDamages => new CharacterStatsModifier { AllDamagesAdditiveModifier = Amount },
            CharacterStatEffectType.ReduceAllDamages => new CharacterStatsModifier { AllDamagesAdditiveModifier = -Amount },
            CharacterStatEffectType.IncreaseAllResistances => new CharacterStatsModifier { AllResistancesAdditiveModifier = Amount },
            CharacterStatEffectType.ReduceAllResistances => new CharacterStatsModifier { AllResistancesAdditiveModifier = -Amount },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum CharacterStatEffectType
{
    IncreaseAllDamages,
    ReduceAllDamages,
    IncreaseAllResistances,
    ReduceAllResistances
}
