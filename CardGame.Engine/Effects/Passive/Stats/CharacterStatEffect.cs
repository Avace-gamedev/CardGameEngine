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
            CharacterStatEffectType.IncreaseResistance => new CharacterStatsModifier { ResistanceAdditiveModifier = Amount },
            CharacterStatEffectType.ReduceResistance => new CharacterStatsModifier { ResistanceAdditiveModifier = -Amount },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum CharacterStatEffectType
{
    IncreaseResistance,
    ReduceResistance
}
