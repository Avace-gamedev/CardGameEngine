using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Enchantments;
using PockedeckBattler.Server.Views.Effects.Enchantments.Passive;
using PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

namespace PockedeckBattler.Server.Views.Effects.Enchantments;

public class EnchantmentView
{
    public EnchantmentView(string name, PassiveEffectView[] passive, TriggeredEffectView[] triggered)
    {
        Name = name;
        Passive = passive;
        Triggered = triggered;
    }

    public string Name { get; }

    [Required]
    public PassiveEffectView[] Passive { get; }

    [Required]
    public TriggeredEffectView[] Triggered { get; }
}

public static class EnchantmentViewMappingExtensions
{
    public static EnchantmentView View(this Enchantment enchantment)
    {
        return new EnchantmentView(enchantment.Name, enchantment.Passive.Select(p => p.View()).ToArray(), enchantment.Triggered.Select(t => t.View()).ToArray());
    }
}
