﻿using CardGame.Engine.Combats.Cards;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Combats.Logs;

public class CombatLog
{
    readonly List<CombatLogEntry> _entries = new();
    public IReadOnlyList<CombatLogEntry> Entries => _entries;

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
}