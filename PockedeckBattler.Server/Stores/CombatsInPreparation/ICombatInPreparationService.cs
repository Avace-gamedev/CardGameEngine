namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public interface ICombatInPreparationService
{
    IAsyncEnumerable<CombatInPreparation> GetCombatsInPreparationInvolvingPlayer(string player, CancellationToken cancellationToken = default);
    Task<CombatInPreparation?> GetCombatInPreparation(Guid guid, CancellationToken cancellationToken = default);
    Task SaveCombatInPreparation(CombatInPreparation combatInPreparation, CancellationToken cancellationToken = default);
    Task DeleteCombatInPreparation(CombatInPreparation id, CancellationToken cancellationToken = default);
}

public static class CombatInPreparationStoreExtensions
{
    public static async Task<CombatInPreparation> RequireCombatInPreparation(this ICombatInPreparationService combatService, Guid guid)
    {
        return await combatService.GetCombatInPreparation(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
