using CardGame.Engine.Effects.Passive.Stats;

namespace CardGame.Engine.Effects.Passive;

public abstract class PassiveEffect
{
    protected PassiveEffect(int duration)
    {
        Duration = duration;
    }

    public int Duration { get; }

    public static PassiveEffect CharacterStatEffect(CharacterStatEffectType type, int value, int duration)
    {
        return new CharacterStatEffect(type, value, duration);
    }

    public static PassiveEffect CardStatEffect(CardStatEffectType type, int value, int duration)
    {
        return new CardStatEffect(type, value, duration);
    }
}
