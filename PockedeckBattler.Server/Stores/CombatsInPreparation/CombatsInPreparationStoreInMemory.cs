using MediatR;
using PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public class CombatsInPreparationStoreInMemory : ICombatsInPreparationStore
{
    readonly Dictionary<Guid, StoredCombatInPreparation> _combatsInPreparationStore = new();
    readonly IMediator _mediator;

    public CombatsInPreparationStoreInMemory(IMediator mediator)
    {
        _mediator = mediator;
    }

    IEnumerable<StoredCombatInPreparation> CombatsInPreparation => _combatsInPreparationStore.Values;

    public IEnumerable<StoredCombatInPreparation> GetCombatsInPreparationInvolvingPlayer(string player)
    {
        return CombatsInPreparation.Where(c => c.LeftPlayerName == player || c.RightPlayerName == player);
    }

    public StoredCombatInPreparation? GetCombatInPreparation(Guid guid)
    {
        return _combatsInPreparationStore.GetValueOrDefault(guid);
    }

    public async Task SaveCombatInPreparation(StoredCombatInPreparation combatInPreparation)
    {
        bool created = !_combatsInPreparationStore.ContainsKey(combatInPreparation.Id);

        _combatsInPreparationStore[combatInPreparation.Id] = combatInPreparation;

        if (created)
        {
            await _mediator.Publish(new CombatInPreparationCreated(combatInPreparation));
        }
        else
        {
            await _mediator.Publish(new CombatInPreparationSaved(combatInPreparation));
        }
    }

    public async Task DeleteCombatInPreparation(Guid id)
    {
        if (!_combatsInPreparationStore.TryGetValue(id, out StoredCombatInPreparation? combatInPreparation))
        {
            return;
        }

        _combatsInPreparationStore.Remove(id);

        await _mediator.Publish(new CombatInPreparationDeleted(combatInPreparation));
    }
}
