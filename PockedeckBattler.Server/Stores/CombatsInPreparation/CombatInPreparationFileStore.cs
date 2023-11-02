namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public class CombatInPreparationFileStore : JsonFileStore<CombatInPreparation>
{
    public CombatInPreparationFileStore() : base(GetDirectory(), null, ".cbt")
    {
    }

    static string GetDirectory()
    {
        return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PockeDeckBattler", "FileStores", "Combats");
    }
}
