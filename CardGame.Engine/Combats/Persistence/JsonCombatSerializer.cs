using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using CardGame.Engine.Combats.History;
using CardGame.Engine.Effects.Active;
using CardGame.Engine.Effects.Enchantments.State;
using CardGame.Engine.Effects.Enchantments.State.Stats;
using CardGame.Engine.Effects.Enchantments.Triggered;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace CardGame.Engine.Combats.Persistence;

public class JsonCombatSerializer : ICombatInstanceSerializer
{
    public async Task<CombatInstance?> DeserializeAsync(Stream stream)
    {
        CombatHistory? history = await JsonSerializer.DeserializeAsync<CombatHistory>(stream, GetSerializerOptions());
        return history?.Replay();
    }

    public async Task SerializeAsync(Stream stream, CombatInstance instance)
    {
        await JsonSerializer.SerializeAsync(stream, instance.History, GetSerializerOptions());
    }

    static JsonSerializerOptions GetSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            TypeInfoResolver = new PolymorphicTypeResolver(),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
#if DEBUG
            WriteIndented = true
#endif
        };
    }

    class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo typeInfo = base.GetTypeInfo(type, options);

            if (typeInfo.Type == typeof(ActiveEffect))
            {
                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "effectType",
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(DamageEffect), "damage"),
                        new JsonDerivedType(typeof(HealEffect), "heal"),
                        new JsonDerivedType(typeof(ShieldEffect), "shield"),
                        new JsonDerivedType(typeof(RandomEffect), "random"),
                        new JsonDerivedType(typeof(AddEnchantmentEffect), "add-enchantment")
                    }
                };
            }
            else if (typeInfo.Type == typeof(EffectTrigger))
            {
                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "triggerType",
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(TurnTrigger), "turn")
                    }
                };
            }
            else if (typeInfo.Type == typeof(PassiveEffect))
            {
                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "effectType",
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(ChangeCardStatEffect), "card-stat"),
                        new JsonDerivedType(typeof(ChangeCharacterStatEffect), "character-stat")
                    }
                };
            }
            else if (typeInfo.Type == typeof(CombatAction))
            {
                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "actionType",
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(PlayCardAction), "play-card"),
                        new JsonDerivedType(typeof(EndTurnAction), "end-turn")
                    }
                };
            }

            return typeInfo;
        }
    }
}
