using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class CharacterDiedLogEntryView : EffectOnCharacterLogEntryView
{
    public CharacterDiedLogEntryView(CharacterInCombatView character) : base(character)
    {
    }
}

public static class CharacterDiedLogEntryViewMappingExtensions
{
    public static CharacterDiedLogEntryView View(this CharacterDiedLogEntry entry)
    {
        return new CharacterDiedLogEntryView(entry.Character.View());
    }
}
