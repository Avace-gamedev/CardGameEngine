namespace PockedeckBattler.Server.Stores.Combats;

public class CombatFileStore : SerializedDataStore<CombatInstanceWithMetadata, SerializableCombatWithMetadata>
{
    public CombatFileStore(ILogger<CombatFileStore> logger, ILogger<JsonFileStore<SerializableCombatWithMetadata>> innerStoreLogger) : base(
        new JsonFileStore<SerializableCombatWithMetadata>(GetDirectory(), innerStoreLogger, null, ".cbt"),
        logger
    )
    {
    }

    static string GetDirectory()
    {
        return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PockeDeckBattler", "FileStores", "Combats");
    }

    protected override async Task<SerializableCombatWithMetadata> Serialize(CombatInstanceWithMetadata value)
    {
        return await SerializableCombatWithMetadata.From(value);
    }

    protected override async Task<CombatInstanceWithMetadata?> Deserialize(SerializableCombatWithMetadata serializedValue)
    {
        return await serializedValue.Restore();
    }
}