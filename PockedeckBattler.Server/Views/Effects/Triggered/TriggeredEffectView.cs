using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Triggered;
using PockedeckBattler.Server.Views.Effects.Active;
using PockedeckBattler.Server.Views.Effects.Passive;

namespace PockedeckBattler.Server.Views.Effects.Triggered;

public class TriggeredEffectView : PassiveEffectView
{
    public TriggeredEffectView(EffectTriggerView trigger, ActiveEffectView effect, int duration) : base(duration)
    {
        Trigger = trigger;
        Effect = effect;
    }

    [Required]
    public EffectTriggerView Trigger { get; }

    [Required]
    public ActiveEffectView Effect { get; }
}

public static class TriggeredEffectViewMappingExtensions
{
    public static TriggeredEffectView TriggeredEffectView(this TriggeredEffect effect)
    {
        return new TriggeredEffectView(effect.Trigger.View(), effect.Effect.View(), effect.Duration);
    }
}
