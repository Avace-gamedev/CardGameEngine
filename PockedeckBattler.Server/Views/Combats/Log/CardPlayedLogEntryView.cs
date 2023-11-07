using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CardPlayedLogEntryView : CombatLogEntryView
{
    public CardPlayedLogEntryView(CharacterInCombatView source, ActionCardView card, params EffectOnCharacterLogEntryView[] effects)
    {
        Card = card;
        Source = source;
        Effects = effects;
    }

    public CharacterInCombatView Source { get; }
    public ActionCardView Card { get; }
    public EffectOnCharacterLogEntryView[] Effects { get; }
}

public static class CardPlayedLogEntryViewMappingExtensions
{
    public static CardPlayedLogEntryView View(this CardPlayedLogEntry entry)
    {
        return new CardPlayedLogEntryView(entry.Source.View(), entry.Card.View(), entry.EffectsOnCharacters.Select(e => e.View()).ToArray());
    }
}
