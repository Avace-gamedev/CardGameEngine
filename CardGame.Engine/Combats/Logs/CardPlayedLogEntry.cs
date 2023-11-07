using CardGame.Engine.Cards.ActionCard;

namespace CardGame.Engine.Combats.Logs;

public class CardPlayedLogEntry : CombatLogEntry
{
    readonly EffectOnCharacterLogEntry[] _effectsOnCharacters;

    public CardPlayedLogEntry(CharacterLogEntry source, ActionCard card, params EffectOnCharacterLogEntry[] effectsOnCharacters)
    {
        Card = card;
        Source = source;
        _effectsOnCharacters = effectsOnCharacters.ToArray();
    }

    public ActionCard Card { get; }
    public CharacterLogEntry Source { get; }
    public IReadOnlyCollection<EffectOnCharacterLogEntry> EffectsOnCharacters => _effectsOnCharacters;
}
