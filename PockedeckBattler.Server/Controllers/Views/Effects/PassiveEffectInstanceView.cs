using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Passive;
using PockedeckBattler.Server.Controllers.Views.Effects.Passive;

namespace PockedeckBattler.Server.Controllers.Views.Effects;

public class PassiveEffectInstanceView
{
    public PassiveEffectInstanceView(Guid id, PassiveEffectView effect, string source, int remainingDuration)
    {
        Id = id;
        Effect = effect;
        Source = source;
        RemainingDuration = remainingDuration;
    }

    public Guid Id { get; }

    [Required]
    public PassiveEffectView Effect { get; }

    public string Source { get; }
    public int RemainingDuration { get; }
}

public static class PassiveEffectInstanceMappingExtensions
{
    public static PassiveEffectInstanceView View(this PassiveEffectInstance instance)
    {
        return new PassiveEffectInstanceView(instance.Id, instance.Effect.View(), instance.Source.Character.Identity.Name, instance.RemainingDuration);
    }
}
