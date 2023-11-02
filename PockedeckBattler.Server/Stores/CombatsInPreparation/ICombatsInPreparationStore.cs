namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public interface ICombatsInPreparationStore
{
    IEnumerable<StoredCombatInPreparation> GetCombatsInPreparationInvolvingPlayer(string player);
    StoredCombatInPreparation? GetCombatInPreparation(Guid guid);
    Task SaveCombatInPreparation(StoredCombatInPreparation combatInPreparation);
    Task DeleteCombatInPreparation(Guid id);
}

public static class CombatInPreparationStoreExtensions
{
    public static StoredCombatInPreparation RequireCombatInPreparation(this ICombatsInPreparationStore combatsStore, Guid guid)
    {
        return combatsStore.GetCombatInPreparation(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
