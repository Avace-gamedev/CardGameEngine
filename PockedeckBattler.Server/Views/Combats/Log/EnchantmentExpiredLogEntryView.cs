using CardGame.Engine.Combats.Logs;
using PockedeckBattler.Server.Views.Effects.Enchantments;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class EnchantmentExpiredLogEntryView : CombatLogEntryView
{
    public EnchantmentExpiredLogEntryView(CharacterInCombatView source, CharacterInCombatView target, EnchantmentView enchantment)
    {
        Source = source;
        Target = target;
        Enchantment = enchantment;
    }

    public CharacterInCombatView Source { get; }
    public CharacterInCombatView Target { get; }
    public EnchantmentView Enchantment { get; }
}

public static class EnchantmentExpiredLogEntryViewMappingExtensions
{
    public static EnchantmentExpiredLogEntryView View(this EnchantmentExpiredLogEntry entry)
    {
        return new EnchantmentExpiredLogEntryView(entry.Source.View(), entry.Target.View(), entry.Enchantment.View());
    }
}
