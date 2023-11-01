using CardGame.Engine.Effects.Active;

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
        int initialDelay = 0,
        TurnTrigger.TriggerMoment triggerMoment = TurnTrigger.TriggerMoment.StartOfTargetTurn
    )
    {
        return new TriggeredEffect(damage, new TurnTrigger(triggerMoment, duration, initialDelay));
    }
}
