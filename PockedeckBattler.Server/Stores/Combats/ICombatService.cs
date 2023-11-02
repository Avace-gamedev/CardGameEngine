namespace PockedeckBattler.Server.Stores.Combats;

public interface ICombatService
{
    IEnumerable<Combat> GetCombatsInvolvingPlayer(string player);
    Combat? GetCombat(Guid guid);
    void SaveCombat(Combat combat);
}

public static class CombatStoreExtensions
{
    public static Combat RequireCombat(this ICombatService combatService, Guid guid)
    {
        return combatService.GetCombat(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }
}
