using CardGame.Engine.Effects.Enchantments;
using CardGame.Engine.Effects.Enchantments.Triggered;

namespace CardGame.Engine.Combats.Logs;

public class TriggeredEffectLogEntry : CombatLogEntry
{
    readonly EffectOnCharacterLogEntry[] _effectsOnCharacters;

    public TriggeredEffectLogEntry(
        CharacterLogEntry source,
        CharacterLogEntry target,
        Enchantment enchantment,
        TriggeredEffect effect,
        params EffectOnCharacterLogEntry[] effectsOnCharacters
    )
    {
        Source = source;
        Target = target;
        Enchantment = enchantment;
        Effect = effect;
        _effectsOnCharacters = effectsOnCharacters;
    }

    public CharacterLogEntry Source { get; }
    public CharacterLogEntry Target { get; }
    public Enchantment Enchantment { get; }
    public TriggeredEffect Effect { get; }
    public IReadOnlyCollection<EffectOnCharacterLogEntry> EffectsOnCharacters => _effectsOnCharacters;
}
