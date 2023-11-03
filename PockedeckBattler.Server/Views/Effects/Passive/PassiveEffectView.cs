using System.Runtime.Serialization;
using CardGame.Engine.Effects.Passive;
using CardGame.Engine.Effects.Passive.Stats;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Effects.Passive;

[JsonConverter(typeof(JsonInheritanceConverter), "type")]
[KnownType(typeof(PassiveStatsModifierView))]
public class PassiveEffectView
{
    public PassiveEffectView(int duration)
    {
        Duration = duration;
    }

    public int Duration { get; }
}

public static class PassiveEffectViewMappingExtensions
{
    public static PassiveEffectView View(this PassiveEffect effect)
    {
        return effect switch
        {
            PassiveStatsModifier passiveStatsModifier => passiveStatsModifier.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(effect))
        };
    }
}
