namespace PockedeckBattler.Server.Stores.Combats;

public interface ICombatsStore
{
    IEnumerable<StoredCombat> GetCombatsInvolvingPlayer(string player);
    StoredCombat? GetCombat(Guid guid);
    void SaveCombat(StoredCombat combat);
}

public static class CombatStoreExtensions
{
    public static StoredCombat RequireCombat(this ICombatsStore combatsStore, Guid guid)
    {
        return combatsStore.GetCombat(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
