using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Enchantments.Triggered;
using PockedeckBattler.Server.Views.Effects.Active;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

public class TriggeredEffectView
{
    public TriggeredEffectView(EffectTriggerView trigger, EffectView effect)
    {
        Trigger = trigger;
        Effect = effect;
    }

    [Required]
    public EffectTriggerView Trigger { get; }

    [Required]
    public EffectView Effect { get; }
}

public static class TriggeredEffectViewMappingExtensions
{
    public static TriggeredEffectView View(this TriggeredEffect effect)
    {
        return new TriggeredEffectView(effect.Trigger.View(), effect.Effect.View());
    }
}
