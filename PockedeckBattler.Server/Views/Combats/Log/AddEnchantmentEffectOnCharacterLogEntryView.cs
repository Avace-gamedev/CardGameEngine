using CardGame.Engine.Combats.Logs;
using PockedeckBattler.Server.Views.Effects.Enchantments;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class AddEnchantmentEffectOnCharacterLogEntryView : EffectOnCharacterLogEntryView
{
    public AddEnchantmentEffectOnCharacterLogEntryView(CharacterInCombatView character, EnchantmentView enchantment) : base(character)
    {
        Enchantment = enchantment;
    }

    public EnchantmentView Enchantment { get; }
}

public static class AddEnchantmentEffectOnCharacterLogEntryViewMappingExtensions
{
    public static AddEnchantmentEffectOnCharacterLogEntryView View(this AddEnchantmentEffectOnCharacterLogEntry entry)
    {
        return new AddEnchantmentEffectOnCharacterLogEntryView(entry.Character.View(), entry.Enchantment.View());
    }
}
