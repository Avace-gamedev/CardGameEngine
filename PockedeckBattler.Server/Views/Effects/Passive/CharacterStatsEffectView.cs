using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Passive.Stats;

namespace PockedeckBattler.Server.Views.Effects.Passive;

public class CharacterStatsEffectView : PassiveEffectView
{
    public CharacterStatsEffectView(CharacterStatEffectType type, int amount, int duration) : base(duration)
    {
        Type = type;
        Amount = amount;
    }

    [Required]
    public CharacterStatEffectType Type { get; }

    public int Amount { get; }
}

public static class CharacterStatsModifierViewMappingExtensions
{
    public static CharacterStatsEffectView View(this CharacterStatEffect effect)
    {
        return new CharacterStatsEffectView(effect.Type, effect.Amount, effect.Duration);
    }
}
