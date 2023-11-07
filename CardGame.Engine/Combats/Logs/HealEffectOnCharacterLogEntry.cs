using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Logs;

public class HealEffectOnCharacterLogEntry : EffectOnCharacterLogEntry
{
    public HealEffectOnCharacterLogEntry(CharacterLogEntry character, HealReceived heal) : base(character)
    {
        Heal = heal;
    }

    public HealReceived Heal { get; }
}
