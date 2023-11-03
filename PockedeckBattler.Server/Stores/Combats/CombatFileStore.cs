using CardGame.Engine.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

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

public class SerializableCombatWithMetadata
{
    public Guid? Id { get; init; }
    public CombatInPreparation? Configuration { get; init; }
    public string? LeftPlayerName { get; init; }
    public string? RightPlayerName { get; init; }
    public SerializableCombatInstance? Instance { get; init; }

    public CombatInstanceWithMetadata? Restore()
    {
        if (!Id.HasValue || string.IsNullOrWhiteSpace(LeftPlayerName) || string.IsNullOrWhiteSpace(RightPlayerName) || Instance == null)
        {
            return null;
        }

        CombatInstance? instance = Instance.Restore();
        if (instance == null)
        {
            return null;
        }

        return new CombatInstanceWithMetadata(Id.Value, LeftPlayerName, RightPlayerName, instance, Configuration);
    }

    public static SerializableCombatWithMetadata From(CombatInstanceWithMetadata combat)
    {
        return new SerializableCombatWithMetadata
        {
            Id = combat.Id,
            Configuration = combat.Configuration,
            LeftPlayerName = combat.LeftPlayerName,
            RightPlayerName = combat.RightPlayerName,
            Instance = SerializableCombatInstance.From(combat.Instance)
        };
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
