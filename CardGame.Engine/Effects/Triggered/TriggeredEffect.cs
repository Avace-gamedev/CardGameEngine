using CardGame.Engine.Effects.Active;
using CardGame.Engine.Effects.Passive;

namespace CardGame.Engine.Effects.Triggered;

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
