using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects;
using PockedeckBattler.Server.Views.Effects.Enchantments;

namespace PockedeckBattler.Server.Views.Effects.Active;

public class AddEnchantmentEffectView : EffectView
{
    public AddEnchantmentEffectView(EnchantmentView passiveEffect)
    {
        EnchantmentEffect = passiveEffect;

    }

    [Required]
    public EnchantmentView EnchantmentEffect { get; }
}

public static class AddEnchantmentEffectViewMappingExtensions
{
    public static AddEnchantmentEffectView View(this AddEnchantmentEffect effect)
    {
        return new AddEnchantmentEffectView(effect.Enchantment.View());
    }
}
