using MediatR;

namespace PockedeckBattler.Server.Stores.Combats;

public class CombatsMemoryStore : ICombatService
{
    readonly Dictionary<Guid, Combat> _combatsStore = new();
    readonly IMediator _mediator;

    public CombatsMemoryStore(IMediator mediator)
    {
        _mediator = mediator;
    }

    IEnumerable<Combat> Combats => _combatsStore.Values;

    public void SaveCombat(Combat combat)
    {
        _combatsStore[combat.Id] = combat;
    }

    public Combat? GetCombat(Guid guid)
    {
        return _combatsStore.GetValueOrDefault(guid);
    }

    public IEnumerable<Combat> GetCombatsInvolvingPlayer(string player)
    {
        return Combats.Where(c => c.LeftPlayerName == player || c.RightPlayerName == player);
    }
}
