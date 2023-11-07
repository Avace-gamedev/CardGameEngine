using System.Runtime.Serialization;
using CardGame.Engine.Effects.Enchantments.Passive;
using CardGame.Engine.Effects.Enchantments.Passive.Stats;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Passive;

[JsonConverter(typeof(JsonInheritanceConverter), "$type")]
[KnownType(typeof(CharacterStatsEffectView))]
[KnownType(typeof(CardStatsEffectView))]
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
            ChangeCharacterStatEffect characterStatsModifier => characterStatsModifier.View(),
            ChangeCardStatEffect cardStatsModifier => cardStatsModifier.View(),
            _ => throw new ArgumentOutOfRangeException(nameof(effect))
        };
    }
}
