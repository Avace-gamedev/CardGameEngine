using CardGameEngine.Combats;

namespace CardGameEngine.Effects.Passive;

public class PassiveEffectInstance
{
    public PassiveEffectInstance(PassiveEffect effect, CharacterCombatState source)
    {
        Effect = effect;
        Source = source;
        RemainingDuration = effect.Duration;
    }

    public PassiveEffect Effect { get; }
    public CharacterCombatState Source { get; }
    public int RemainingDuration { get; set; }
}
