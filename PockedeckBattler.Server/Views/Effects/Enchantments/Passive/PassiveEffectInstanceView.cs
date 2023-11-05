using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Enchantments.State;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Passive;

public class PassiveEffectInstanceView
{
    public PassiveEffectInstanceView(Guid id, PassiveEffectView effect, CharacterInCombatView source, int remainingDuration)
    {
        Id = id;
        Effect = effect;
        Source = source;
        RemainingDuration = remainingDuration;
    }

    public Guid Id { get; }

    [Required]
    public PassiveEffectView Effect { get; }

    [Required]
    public CharacterInCombatView Source { get; }

    public int RemainingDuration { get; }
}

public static class PassiveEffectInstanceMappingExtensions
{
    public static PassiveEffectInstanceView View(this PassiveEffectInstance instance)
    {
        return new PassiveEffectInstanceView(instance.Id, instance.Effect.View(), instance.Source.InCombatView(), instance.RemainingDuration);
    }
}
