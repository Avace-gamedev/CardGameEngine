﻿using System.Runtime.Serialization;
using CardGame.Engine.Combats.Logs;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Combats.Log;

[JsonConverter(typeof(JsonInheritanceConverter), "entryType")]
[KnownType(typeof(CardPlayedLogEntryView))]
[KnownType(typeof(TriggeredEffectLogEntryView))]
[KnownType(typeof(EnchantmentExpiredLogEntryView))]
[KnownType(typeof(CombatEndedLogEntryView))]
public abstract class CombatLogEntryView
{
}

public static class CombatLogEntryViewMappingExtensions
{
    public static CombatLogEntryView View(this CombatLogEntry entry)
    {
        return entry switch
        {
            CardPlayedLogEntry cardPlayedLogEntry => cardPlayedLogEntry.View(),
            TriggeredEffectLogEntry triggeredEffectLogEntry => triggeredEffectLogEntry.View(),
            EnchantmentExpiredLogEntry enchantmentExpiredLogEntry => enchantmentExpiredLogEntry.View(),
            CombatEndedLogEntry combatEndedLogEntry => combatEndedLogEntry.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(entry))
        };
    }
}
