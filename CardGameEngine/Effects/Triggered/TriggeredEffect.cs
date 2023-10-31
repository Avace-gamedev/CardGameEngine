using CardGameEngine.Effects.Active;
using CardGameEngine.Effects.Passive;

namespace CardGameEngine.Effects.Triggered;

public class TriggeredEffect : PassiveEffect
{
    public TriggeredEffect(ActiveEffect effect, EffectTrigger trigger, int duration) : base(duration)
    {
        Effect = effect;
        Trigger = trigger;
    }

    EffectTrigger Trigger { get; }
    ActiveEffect Effect { get; }
}
