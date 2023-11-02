using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Rest.Combats;

public interface ICombatService
{
    Task<CombatWithMetadata> CreateCombat(CombatInPreparation combatInPreparation, CancellationToken cancellationToken = default);
    IAsyncEnumerable<CombatWithMetadata> GetCombatsInvolvingPlayer(string player, CancellationToken cancellationToken = default);
    Task<CombatWithMetadata?> GetCombat(Guid id, CancellationToken cancellationToken = default);
    Task SaveCombat(CombatWithMetadata combat, CancellationToken cancellationToken = default);
}

public static class CombatStoreExtensions
{
    public static async Task<CombatWithMetadata> RequireCombat(this ICombatService combatService, Guid guid)
    {
        return await combatService.GetCombat(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
