using System.Runtime.Serialization;
using CardGame.Engine.Effects.Passive;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using PockedeckBattler.Server.Controllers.Views.Effects.Triggered;

namespace PockedeckBattler.Server.Controllers.Views.Effects.Passive;

[JsonConverter(typeof(JsonInheritanceConverter), "type")]
[KnownType(typeof(PassiveStatsModifierView))]
[KnownType(typeof(TriggeredEffectView))]
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
