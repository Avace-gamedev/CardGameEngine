using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Stores;

public static class CombatStore
{
    static readonly Dictionary<Guid, CombatInstance> Combats = new();

    public static IEnumerable<Guid> AllIds => Combats.Keys;
    public static IEnumerable<CombatInstance> All => Combats.Values;

    public static Guid Register(CombatInstance combat)
    {
        Guid guid = Guid.NewGuid();
        Combats[guid] = combat;

        return guid;
    }

    public static CombatInstance? Get(Guid guid)
    {
        return Combats.GetValueOrDefault(guid);
    }

    public static CombatInstance Require(Guid guid)
    {
        return Get(guid) ?? throw new Exception($"Cannot find combat {guid}");
    }
}
