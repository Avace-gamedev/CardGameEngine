﻿using System.Runtime.Serialization;
using CardGame.Engine.Effects.Enchantments.Triggered;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

[JsonConverter(typeof(JsonInheritanceConverter), "$type")]
[KnownType(typeof(TurnTriggerView))]
public class EffectTriggerView
{
}

public static class EffectTriggerViewMappingExtensions
{
    public static EffectTriggerView View(this EffectTrigger effect)
    {
        return effect switch
        {
            TurnTrigger turnTrigger => turnTrigger.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(effect))
        };
    }
}
