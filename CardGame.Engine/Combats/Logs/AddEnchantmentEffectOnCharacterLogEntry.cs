using CardGame.Engine.Effects.Enchantments;

namespace CardGame.Engine.Combats.Logs;

public class AddEnchantmentEffectOnCharacterLogEntry : EffectOnCharacterLogEntry
{
    public AddEnchantmentEffectOnCharacterLogEntry(CharacterLogEntry character, Enchantment enchantment) : base(character)
    {
        Enchantment = enchantment;
    }

    public Enchantment Enchantment { get; }
}
