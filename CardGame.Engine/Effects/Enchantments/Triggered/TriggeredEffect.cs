using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace CardGame.Engine.Effects.Enchantments.Triggered;

public class TriggeredEffect
{
    public TriggeredEffect(Effect effect, EffectTrigger trigger)
    {
        Effect = effect;
        Trigger = trigger;
    }

    public EffectTrigger Trigger { get; }
    public Effect Effect { get; }

    public static TriggeredEffect DamageOverTime(
        DamageEffect damage,
        int duration,
        TurnTrigger.TriggerMoment triggerMoment = TurnTrigger.TriggerMoment.StartOfTargetTurn
    )
    {
        return new TriggeredEffect(damage, new TurnTrigger(triggerMoment, duration));
    }

    public static TriggeredEffect DelayedDamage(
        DamageEffect damage,
        int delay,
        TurnTrigger.TriggerMoment triggerMoment = TurnTrigger.TriggerMoment.StartOfTargetTurn
    )
    {
        return new TriggeredEffect(damage, new TurnTrigger(triggerMoment, 1, delay));
    }
}
