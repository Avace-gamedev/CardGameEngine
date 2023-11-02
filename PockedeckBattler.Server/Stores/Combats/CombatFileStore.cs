namespace PockedeckBattler.Server.Stores.Combats;

public class CombatFileStore : JsonFileStore<CombatWithMetadata>
{
    public CombatFileStore(ILogger<CombatFileStore> logger) : base(GetDirectory(), logger, null, ".cbt")
    {
    }

    static string GetDirectory()
    {
        return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PockeDeckBattler", "FileStores", "Combats");
    }

    protected override bool Validate(CombatWithMetadata value)
    {
        return !string.IsNullOrWhiteSpace(value.LeftPlayerName) && !string.IsNullOrWhiteSpace(value.RightPlayerName) && value.Instance != null;
    }
}
