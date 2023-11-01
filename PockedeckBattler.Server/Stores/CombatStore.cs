namespace PockedeckBattler.Server.Stores;

public static class CombatStore
{
    static readonly Dictionary<Guid, StoredCombatInPreparation> CombatsInPreparationStore = new();
    static readonly Dictionary<Guid, StoredCombat> CombatsStore = new();

    public static IEnumerable<StoredCombat> Combats => CombatsStore.Values;
    public static IEnumerable<StoredCombatInPreparation> CombatsInPreparation => CombatsInPreparationStore.Values;

    public static void SaveCombatInPreparation(StoredCombatInPreparation combatInPreparation)
    {
        CombatsInPreparationStore[combatInPreparation.Id] = combatInPreparation;
    }

    public static StoredCombatInPreparation? GetCombatInPreparation(Guid guid)
    {
        return CombatsInPreparationStore.GetValueOrDefault(guid);
    }

    public static StoredCombatInPreparation RequireCombatInPreparation(Guid guid)
    {
        return GetCombatInPreparation(guid) ?? throw new Exception($"Cannot find combat in preparation {guid}");
    }

    public static void DeleteCombatInPreparation(Guid id)
    {
        CombatsInPreparationStore.Remove(id);
    }

    public static IEnumerable<StoredCombatInPreparation> GetCombatsInPreparationInvolvingPlayer(string player)
    {
        return CombatsInPreparation.Where(c => c.LeftPlayerName == player || c.RightPlayerName == player);
    }

    public static void SaveCombat(StoredCombat combat)
    {
        CombatsStore[combat.Id] = combat;
    }

    public static StoredCombat? GetCombat(Guid guid)
    {
        return CombatsStore.GetValueOrDefault(guid);
    }

    public static StoredCombat RequireCombat(Guid guid)
    {
        return GetCombat(guid) ?? throw new Exception($"Cannot find combat {guid}");
    }

    public static IEnumerable<StoredCombat> GetCombatsInvolvingPlayer(string player)
    {
        return Combats.Where(c => c.LeftPlayerName == player || c.RightPlayerName == player);
    }
}
