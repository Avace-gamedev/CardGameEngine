using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class DamageEffectOnCharacterLogEntryView : EffectOnCharacterLogEntryView
{
    public DamageEffectOnCharacterLogEntryView(CharacterInCombatView character, DamageReceived damage) : base(character)
    {
        Damage = damage;
    }

    public DamageReceived Damage { get; }
}

public static class DamageEffectOnCharacterLogEntryViewMappingExtensions
{
    public static DamageEffectOnCharacterLogEntryView View(this DamageEffectOnCharacterLogEntry entry)
    {
        return new DamageEffectOnCharacterLogEntryView(entry.Character.View(), entry.Damage);
    }
}
