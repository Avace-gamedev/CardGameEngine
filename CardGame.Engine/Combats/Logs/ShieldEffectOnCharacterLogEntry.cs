using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Logs;

public class ShieldEffectOnCharacterLogEntry : EffectOnCharacterLogEntry
{
    public ShieldEffectOnCharacterLogEntry(CharacterLogEntry character, ShieldReceived shield) : base(character)
    {
        Shield = shield;
    }

    public ShieldReceived Shield { get; }
}
