using CardGame.Engine.Combats.Logs;
using PockedeckBattler.Server.Views.Effects.Enchantments;
using PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class TriggeredEffectLogEntryView : CombatLogEntryView
{
    public TriggeredEffectLogEntryView(
        CharacterInCombatView source,
        CharacterInCombatView target,
        EnchantmentView enchantment,
        TriggeredEffectView effect,
        params EffectOnCharacterLogEntryView[] effectsOnCharacters
    )
    {
        Source = source;
        Target = target;
        Enchantment = enchantment;
        Effect = effect;
        EffectsOnCharacters = effectsOnCharacters;
    }

    public CharacterInCombatView Source { get; }
    public CharacterInCombatView Target { get; }
    public EnchantmentView Enchantment { get; }
    public TriggeredEffectView Effect { get; }
    public EffectOnCharacterLogEntryView[] EffectsOnCharacters { get; }
}

public static class TriggeredEffectLogEntryViewMappingExtensions
{
    public static TriggeredEffectLogEntryView View(this TriggeredEffectLogEntry entry)
    {
        return new TriggeredEffectLogEntryView(
            entry.Source.View(),
            entry.Target.View(),
            entry.Enchantment.View(),
            entry.Effect.View(),
            entry.EffectsOnCharacters.Select(e => e.View()).ToArray()
        );
    }
}
