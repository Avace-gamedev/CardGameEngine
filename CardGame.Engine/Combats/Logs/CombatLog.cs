using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Cards;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects.Enchantments;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace CardGame.Engine.Combats.Logs;

public class CombatLog
{
    readonly List<CombatLogEntry> _entries = new();
    public IReadOnlyList<CombatLogEntry> Entries => _entries;

    public void RecordPhaseChange(int turn, CombatSide side, CombatSideTurnPhase phase)
    {
        _entries.Add(new TurnPhaseChangedLogEntry(turn, side, phase));
    }

    public IDisposable RecordEffectsOfPlayingCard(ActionCardInstance card)
    {
        CharacterCombatState source = card.Character;
        return new EffectsOnCharacterRecorder(
            source.Combat,
            effects =>
            {
                CardPlayedLogEntry characterEntry = new(
                    new CharacterLogEntry(source.Character.Identity.Name, source.Side),
                    card.GetCardWithModifications(),
                    effects.ToArray()
                );

                _entries.Add(characterEntry);
            }
        );
    }

    public IDisposable RecordTriggeredEffect(TriggeredEffectInstance instance)
    {
        return new EffectsOnCharacterRecorder(
            instance.Source.Combat,
            effects =>
            {
                TriggeredEffectLogEntry entry = new(
                    new CharacterLogEntry(instance.Source.Character.Identity.Name, instance.Source.Side),
                    new CharacterLogEntry(instance.Target.Character.Identity.Name, instance.Target.Side),
                    instance.Enchantment.Enchantment,
                    instance.Effect,
                    effects.ToArray()
                );

                _entries.Add(entry);
            }
        );
    }

    public void RecordEnchantmentExpired(EnchantmentInstance instance)
    {
        _entries.Add(
            new EnchantmentExpiredLogEntry(
                new CharacterLogEntry(instance.Source.Character.Identity.Name, instance.Source.Side),
                new CharacterLogEntry(instance.Target.Character.Identity.Name, instance.Target.Side),
                instance.Enchantment
            )
        );
    }

    public void RecordEnd(CombatSide winner)
    {
        _entries.Add(new CombatEndedLogEntry(winner));
    }
}
