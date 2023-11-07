namespace CardGame.Engine.Combats.Logs;

public abstract class EffectOnCharacterLogEntry
{
    protected EffectOnCharacterLogEntry(CharacterLogEntry character)
    {
        Character = character;
    }

    public CharacterLogEntry Character { get; }
}
