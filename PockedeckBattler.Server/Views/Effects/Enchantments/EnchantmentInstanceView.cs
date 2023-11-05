using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Enchantments;
using PockedeckBattler.Server.Views.Effects.Enchantments.Passive;
using PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

namespace PockedeckBattler.Server.Views.Effects.Enchantments;

public class EnchantmentInstanceView
{
    public EnchantmentInstanceView(
        Guid id,
        EnchantmentView enchantment,
        CharacterInCombatView source,
        CharacterInCombatView target,
        PassiveEffectInstanceView[] passive,
        TriggeredEffectInstanceView[] triggered
    )
    {
        Id = id;
        Enchantment = enchantment;
        Source = source;
        Target = target;
        Passive = passive;
        Triggered = triggered;
    }

    public Guid Id { get; }

    [Required]
    public EnchantmentView Enchantment { get; }

    [Required]
    public CharacterInCombatView Source { get; }

    [Required]
    public CharacterInCombatView Target { get; }

    [Required]
    public PassiveEffectInstanceView[] Passive { get; }

    [Required]
    public TriggeredEffectInstanceView[] Triggered { get; }
}

public static class EnchantmentInstanceViewMappingExtensions
{
    public static EnchantmentInstanceView View(this EnchantmentInstance enchantmentInstance)
    {
        return new EnchantmentInstanceView(
            enchantmentInstance.Id,
            enchantmentInstance.Enchantment.View(),
            enchantmentInstance.Source.InCombatView(),
            enchantmentInstance.Target.InCombatView(),
            enchantmentInstance.PassiveEffects.Select(p => p.View()).ToArray(),
            enchantmentInstance.TriggeredEffects.Select(t => t.View()).ToArray()
        );
    }
}
