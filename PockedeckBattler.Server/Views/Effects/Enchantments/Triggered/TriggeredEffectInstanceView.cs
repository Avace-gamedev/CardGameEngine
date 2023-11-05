using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

public class TriggeredEffectInstanceView
{
    public TriggeredEffectInstanceView(Guid id, TriggeredEffectView effect, CharacterInCombatView source, TriggerStateView triggerState)
    {
        Id = id;
        Effect = effect;
        Source = source;
        TriggerState = triggerState;
    }

    public Guid Id { get; }

    [Required]
    public TriggeredEffectView Effect { get; }

    public CharacterInCombatView Source { get; }

    [Required]
    public TriggerStateView TriggerState { get; }
}

public static class TriggeredEffectInstanceMappingExtensions
{
    public static TriggeredEffectInstanceView View(this TriggeredEffectInstance instance)
    {
        return new TriggeredEffectInstanceView(instance.Id, instance.Effect.View(), instance.Source.InCombatView(), instance.TriggerState.View());
    }
}
