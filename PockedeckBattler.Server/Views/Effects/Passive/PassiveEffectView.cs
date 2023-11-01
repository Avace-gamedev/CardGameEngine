using System.Runtime.Serialization;
using CardGame.Engine.Effects.Passive;
using CardGame.Engine.Effects.Triggered;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using PockedeckBattler.Server.Views.Effects.Triggered;

namespace PockedeckBattler.Server.Views.Effects.Passive;

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
            TriggeredEffect triggeredEffect => triggeredEffect.TriggeredEffectView(),
            _ => throw new ArgumentOutOfRangeException(nameof(effect))
        };
    }
}
