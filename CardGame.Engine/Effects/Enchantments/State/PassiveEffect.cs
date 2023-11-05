using CardGame.Engine.Effects.Enchantments.State.Stats;

namespace CardGame.Engine.Effects.Enchantments.State;

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
