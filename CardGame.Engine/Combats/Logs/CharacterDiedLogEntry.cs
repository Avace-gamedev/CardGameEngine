namespace CardGame.Engine.Combats.Logs;

public class CharacterDiedLogEntry : EffectOnCharacterLogEntry
{
    public CharacterDiedLogEntry(CharacterLogEntry character) : base(character)
    {
    }
}
