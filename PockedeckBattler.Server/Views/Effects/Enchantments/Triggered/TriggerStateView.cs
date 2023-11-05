using System.Runtime.Serialization;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

[JsonConverter(typeof(JsonInheritanceConverter), "$type")]
[KnownType(typeof(TurnTriggerStateView))]
public abstract class TriggerStateView
{
}

public static class TriggerStateViewMappingExtensions
{
    public static TriggerStateView View(this TriggerState state)
    {
        return state switch
        {
            TurnTrigger.State turnTriggerState => turnTriggerState.View()
        };
    }
}
