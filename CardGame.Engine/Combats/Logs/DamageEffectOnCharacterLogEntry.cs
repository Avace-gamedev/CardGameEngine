using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Logs;

public class DamageEffectOnCharacterLogEntry : EffectOnCharacterLogEntry
{
    public DamageEffectOnCharacterLogEntry(CharacterLogEntry character, DamageReceived damage) : base(character)
    {
        Damage = damage;
    }

    public DamageReceived Damage { get; }
}
