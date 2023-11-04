using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Passive.Stats;

namespace PockedeckBattler.Server.Views.Effects.Passive;

public class CardStatsEffectView : PassiveEffectView
{
    public CardStatsEffectView(CardStatEffectType type, int amount, int duration) : base(duration)
    {
        Type = type;
        Amount = amount;
    }

    [Required]
    public CardStatEffectType Type { get; }

    public int Amount { get; }
}

public static class CardStatsModifierViewMappingExtensions
{
    public static CardStatsEffectView View(this CardStatEffect effect)
    {
        return new CardStatsEffectView(effect.Type, effect.Amount, effect.Duration);
    }
}
