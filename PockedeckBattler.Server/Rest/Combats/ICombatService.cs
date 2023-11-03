using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Rest.Combats;

public interface ICombatService
{
    Task<CombatInstanceWithMetadata> CreateCombat(CombatInPreparation combatInPreparation, CancellationToken cancellationToken = default);
    IAsyncEnumerable<CombatInstanceWithMetadata> GetCombatsInvolvingPlayer(string player, CancellationToken cancellationToken = default);
    Task<CombatInstanceWithMetadata?> GetCombat(Guid id, CancellationToken cancellationToken = default);
    Task SaveCombat(CombatInstanceWithMetadata combat, CancellationToken cancellationToken = default);
}

public static class CombatStoreExtensions
{
    public static async Task<CombatInstanceWithMetadata> RequireCombat(this ICombatService combatService, Guid guid)
    {
        return await combatService.GetCombat(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
