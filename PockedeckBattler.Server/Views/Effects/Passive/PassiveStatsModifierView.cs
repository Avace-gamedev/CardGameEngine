using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Passive.Stats;

namespace PockedeckBattler.Server.Views.Effects.Passive;

public class PassiveStatsModifierView : PassiveEffectView
{
    public PassiveStatsModifierView(StatEffect effect, int amount, int duration) : base(duration)
    {
        Effect = effect;
        Amount = amount;
    }

    [Required]
    public StatEffect Effect { get; }

    public int Amount { get; }
}

public static class PassiveStatsModifierViewMappingExtensions
{
    public static PassiveStatsModifierView View(this PassiveStatsModifier modifier)
    {
        return new PassiveStatsModifierView(modifier.Effect, modifier.Amount, modifier.Duration);
    }
}
