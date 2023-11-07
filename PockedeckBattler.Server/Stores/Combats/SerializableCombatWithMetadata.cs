using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Serialization;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Stores.Combats;

public class SerializableCombatWithMetadata
{
    static readonly ICombatInstanceSerializer Serializer = new JsonCombatSerializer();

    public Guid? Id { get; init; }
    public CombatInPreparation? Configuration { get; init; }
    public string? LeftPlayerName { get; init; }
    public string? RightPlayerName { get; init; }
    public string? SerializedCombatInstanceAsBase64 { get; init; }

    public async Task<CombatInstanceWithMetadata?> Restore()
    {
        if (!Id.HasValue || string.IsNullOrWhiteSpace(LeftPlayerName) || string.IsNullOrWhiteSpace(RightPlayerName) || SerializedCombatInstanceAsBase64 == null)
        {
            return null;
        }

        byte[] serializedCombatBytes = Convert.FromBase64String(SerializedCombatInstanceAsBase64);

        CombatInstance? combatInstance;
        using (MemoryStream stream = new(serializedCombatBytes))
        {
            combatInstance = await Serializer.DeserializeAsync(stream);
        }

        if (combatInstance == null)
        {
            return null;
        }

        return new CombatInstanceWithMetadata(Id.Value, LeftPlayerName, RightPlayerName, combatInstance, Configuration);
    }

    public static async Task<SerializableCombatWithMetadata> From(CombatInstanceWithMetadata combat)
    {
        string serializedCombatAsBase64;
        using (MemoryStream stream = new())
        {
            await Serializer.SerializeAsync(stream, combat.Instance);
            stream.Seek(0, SeekOrigin.Begin);

            serializedCombatAsBase64 = Convert.ToBase64String(stream.ToArray());
        }

        return new SerializableCombatWithMetadata
        {
            Id = combat.Id,
            Configuration = combat.Configuration,
            LeftPlayerName = combat.LeftPlayerName,
            RightPlayerName = combat.RightPlayerName,
            SerializedCombatInstanceAsBase64 = serializedCombatAsBase64
        };
    }
}
