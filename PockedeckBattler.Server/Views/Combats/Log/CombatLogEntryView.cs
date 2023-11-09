using System.Runtime.Serialization;
using CardGame.Engine.Combats.Logs;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Combats.Log;

[JsonConverter(typeof(JsonInheritanceConverter), "entryType")]
[KnownType(typeof(CombatTurnStartedLogEntryView))]
[KnownType(typeof(CardPlayedLogEntryView))]
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
            CombatTurnStartedLogEntry combatTurnStartedLogEntry => combatTurnStartedLogEntry.View(),
            CardPlayedLogEntry cardPlayedLogEntry => cardPlayedLogEntry.View(),
            CombatEndedLogEntry combatEndedLogEntry => combatEndedLogEntry.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(entry))
        };
    }
}
