using CardGame.Engine.Combats;

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

    protected override SerializableCombatWithMetadata Serialize(CombatInstanceWithMetadata value)
    {
        return SerializableCombatWithMetadata.From(value);
    }

    protected override CombatInstanceWithMetadata? Deserialize(SerializableCombatWithMetadata serializedValue)
    {
        return serializedValue.Restore();
    }
}

public class SerializableCombatInstance
{
    public CombatInstance? Restore()
    {
        return null;
    }

    public static SerializableCombatInstance From(CombatInstance instance)
    {
        return new SerializableCombatInstance();
    }
}
