using CardGame.Engine.Effects.Active;
using CardGame.Engine.Effects.Triggered.Instance;

namespace CardGame.Engine.Effects.Triggered;

public class TriggeredEffect
{
    public TriggeredEffect(ActiveEffect effect, EffectTrigger trigger)
    {
        Effect = effect;
        Trigger = trigger;
    }

    public EffectTrigger Trigger { get; }
    public ActiveEffect Effect { get; }

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
        return new TriggeredEffect(damage, new TurnTrigger(triggerMoment, 0, delay));
    }
}
