using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Passive;

public class PassiveEffectInstance
{
    public PassiveEffectInstance(PassiveEffect effect, CharacterCombatState source)
    {
        Id = Guid.NewGuid();
        Effect = effect;
        Source = source;
        RemainingDuration = effect.Duration;
    }

    public Guid Id { get; }
    public PassiveEffect Effect { get; }
    public CharacterCombatState Source { get; }
    public int RemainingDuration { get; set; }
}
