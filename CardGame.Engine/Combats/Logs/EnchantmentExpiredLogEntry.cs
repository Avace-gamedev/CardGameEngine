using CardGame.Engine.Effects.Enchantments;

namespace CardGame.Engine.Combats.Logs;

public class EnchantmentExpiredLogEntry : CombatLogEntry
{
    public EnchantmentExpiredLogEntry(CharacterLogEntry source, CharacterLogEntry target, Enchantment enchantment)
    {
        Source = source;
        Target = target;
        Enchantment = enchantment;
    }

    public CharacterLogEntry Source { get; }
    public CharacterLogEntry Target { get; }
    public Enchantment Enchantment { get; }
}
