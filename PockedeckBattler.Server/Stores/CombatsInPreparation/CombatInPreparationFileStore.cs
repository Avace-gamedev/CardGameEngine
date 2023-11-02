namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public class CombatInPreparationFileStore : JsonFileStore<CombatInPreparation>
{
    public CombatInPreparationFileStore(ILogger<CombatInPreparationFileStore> logger) : base(GetDirectory(), logger, null, ".cbt.config")
    {
    }

    static string GetDirectory()
    {
        return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PockeDeckBattler", "FileStores", "Combats");
    }

    protected override bool Validate(CombatInPreparation value)
    {
        return !string.IsNullOrWhiteSpace(value.LeftPlayerName);
    }
}
