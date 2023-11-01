using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Triggered;

public class TriggeredEffectInstance
{
    public TriggeredEffectInstance(TriggeredEffect effect, CharacterCombatState source)
    {
        Id = Guid.NewGuid();
        Effect = effect;
        Source = source;
        TriggerState = effect.Trigger.CreateNewState();
    }

    public Guid Id { get; }
    public TriggeredEffect Effect { get; }
    public CharacterCombatState Source { get; }
    public TriggerState TriggerState { get; }
}

public abstract class TriggerState
{
}
