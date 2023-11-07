using System.Runtime.Serialization;
using CardGame.Engine.Combats.Logs;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Combats.Log;

[JsonConverter(typeof(JsonInheritanceConverter), "effectType")]
[KnownType(typeof(DamageEffectOnCharacterLogEntryView))]
[KnownType(typeof(HealEffectOnCharacterLogEntryView))]
[KnownType(typeof(ShieldEffectOnCharacterLogEntryView))]
[KnownType(typeof(AddEnchantmentEffectOnCharacterLogEntryView))]
public abstract class EffectOnCharacterLogEntryView
{
    protected EffectOnCharacterLogEntryView(CharacterInCombatView character)
    {
        Character = character;
    }

    public CharacterInCombatView Character { get; }
}

public static class EffectOnCharacterLogEntryViewMappingExtensions
{
    public static EffectOnCharacterLogEntryView View(this EffectOnCharacterLogEntry entry)
    {
        return entry switch
        {
            DamageEffectOnCharacterLogEntry damageEffectOnCharacterLogEntry => damageEffectOnCharacterLogEntry.View(),
            HealEffectOnCharacterLogEntry healEffectOnCharacterLogEntry => healEffectOnCharacterLogEntry.View(),
            ShieldEffectOnCharacterLogEntry shieldEffectOnCharacterLogEntry => shieldEffectOnCharacterLogEntry.View(),
            AddEnchantmentEffectOnCharacterLogEntry addEnchantmentEffectOnCharacterLogEntry => addEnchantmentEffectOnCharacterLogEntry.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(entry))
        };
    }
}
