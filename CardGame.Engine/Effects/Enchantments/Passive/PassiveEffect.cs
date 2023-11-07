using CardGame.Engine.Effects.Enchantments.Passive.Stats;

namespace CardGame.Engine.Effects.Enchantments.Passive;

public abstract class PassiveEffect
{
    protected PassiveEffect(int duration)
    {
        Duration = duration;
    }

    public int Duration { get; }

    public static PassiveEffect CharacterStatEffect(CharacterStatEffectType type, int value, int duration)
    {
        return new ChangeCharacterStatEffect(type, value, duration);
    }

    public static PassiveEffect CardStatEffect(CardStatEffectType type, int value, int duration)
    {
        return new ChangeCardStatEffect(type, value, duration);
    }
}
