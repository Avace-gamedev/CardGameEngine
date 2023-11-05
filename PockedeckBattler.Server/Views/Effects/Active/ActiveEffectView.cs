using System.Runtime.Serialization;
using CardGame.Engine.Effects.Active;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Effects.Active;

[JsonConverter(typeof(JsonInheritanceConverter), "$type")]
[KnownType(typeof(DamageEffectView))]
[KnownType(typeof(HealEffectView))]
[KnownType(typeof(ShieldEffectView))]
[KnownType(typeof(AddEnchantmentEffectView))]
[KnownType(typeof(RandomEffectView))]
public abstract class ActiveEffectView
{
}

public static class ActiveEffectViewMappingExtensions
{
    public static ActiveEffectView View(this ActiveEffect effect)
    {
        return effect switch
        {
            AddEnchantmentEffect addEnchantmentEffect => addEnchantmentEffect.View(),
            DamageEffect damageEffect => damageEffect.View(),
            HealEffect healEffect => healEffect.View(),
            RandomEffect randomEffect => randomEffect.View(),
            ShieldEffect shieldEffect => shieldEffect.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(effect))
        };
    }
}
