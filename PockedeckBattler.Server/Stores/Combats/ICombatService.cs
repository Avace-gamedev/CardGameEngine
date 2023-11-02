namespace PockedeckBattler.Server.Stores.Combats;

public interface ICombatService
{
    IAsyncEnumerable<Combat> GetCombatsInvolvingPlayer(string player, CancellationToken cancellationToken = default);
    Task<Combat?> GetCombat(Guid id, CancellationToken cancellationToken = default);
    Task SaveCombat(Combat combat, CancellationToken cancellationToken = default);
}

public static class CombatStoreExtensions
{
    public static async Task<Combat> RequireCombat(this ICombatService combatService, Guid guid)
    {
        return await combatService.GetCombat(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
