using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Triggered;
using PockedeckBattler.Server.Controllers.Views.Effects.Active;

namespace PockedeckBattler.Server.Controllers.Views.Effects.Triggered;

public class TriggeredEffectView
{
    public TriggeredEffectView(EffectTriggerView trigger, ActiveEffectView effect)
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
    public static TriggeredEffectView View(this TriggeredEffect effect)
    {
        return new TriggeredEffectView(effect.Trigger.View(), effect.Effect.View());
    }
}
