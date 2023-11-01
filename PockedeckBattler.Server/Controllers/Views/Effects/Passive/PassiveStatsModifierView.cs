using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Passive;

namespace PockedeckBattler.Server.Controllers.Views.Effects.Passive;

public class PassiveStatsModifierView : PassiveEffectView
{
    public PassiveStatsModifierView(StatsModifier statsModifier, int duration) : base(duration)
    {
        StatsModifier = statsModifier;
    }

    [Required]
    public StatsModifier StatsModifier { get; }
}

public static class PassiveStatsModifierViewMappingExtensions
{
    public static PassiveStatsModifierView View(this PassiveStatsModifier modifier)
    {
        return new PassiveStatsModifierView(modifier.StatsModifier, modifier.Duration);
    }
}
