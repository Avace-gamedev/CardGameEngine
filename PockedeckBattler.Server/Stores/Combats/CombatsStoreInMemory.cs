using MediatR;

namespace PockedeckBattler.Server.Stores.Combats;

public class CombatsStoreInMemory : ICombatsStore
{
    readonly Dictionary<Guid, StoredCombat> _combatsStore = new();
    readonly IMediator _mediator;

    public CombatsStoreInMemory(IMediator mediator)
    {
        _mediator = mediator;
    }

    IEnumerable<StoredCombat> Combats => _combatsStore.Values;

    public void SaveCombat(StoredCombat combat)
    {
        _combatsStore[combat.Id] = combat;
    }

    public StoredCombat? GetCombat(Guid guid)
    {
        return _combatsStore.GetValueOrDefault(guid);
    }

    public IEnumerable<StoredCombat> GetCombatsInvolvingPlayer(string player)
    {
        return Combats.Where(c => c.LeftPlayerName == player || c.RightPlayerName == player);
    }
}
