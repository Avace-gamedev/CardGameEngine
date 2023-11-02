using System.Runtime.CompilerServices;
using MediatR;
using PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public class CombatInPreparationService : ICombatInPreparationService
{
    readonly IMediator _mediator;
    readonly IStore<CombatInPreparation> _store;

    public CombatInPreparationService(IStore<CombatInPreparation> store, IMediator mediator)
    {
        _store = store;
        _mediator = mediator;
    }

    public async IAsyncEnumerable<CombatInPreparation> GetCombatsInPreparationInvolvingPlayer(
        string player,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        await foreach (CombatInPreparation combat in _store.LoadAll(cancellationToken))
        {
            if (combat.LeftPlayerName == player || combat.RightPlayerName == player)
            {
                yield return combat;
            }
        }
    }

    public async Task<CombatInPreparation?> GetCombatInPreparation(Guid guid, CancellationToken cancellationToken = default)
    {
        return await _store.Load(GetKey(guid), cancellationToken);
    }

    public async Task SaveCombatInPreparation(CombatInPreparation combatInPreparation, CancellationToken cancellationToken = default)
    {
        string key = GetKey(combatInPreparation.Id);
        bool created = !await _store.Exists(key, cancellationToken);

        await _store.Save(key, combatInPreparation, cancellationToken);

        if (created)
        {
            await _mediator.Publish(new CombatInPreparationCreated(combatInPreparation), cancellationToken);
        }
        else
        {
            await _mediator.Publish(new CombatInPreparationSaved(combatInPreparation), cancellationToken);
        }
    }

    public async Task DeleteCombatInPreparation(Guid id, CancellationToken cancellationToken = default)
    {
        string key = GetKey(id);
        CombatInPreparation? combatInPreparation = await _store.Load(key, cancellationToken);
        if (combatInPreparation == null)
        {
            return;
        }

        await _store.Delete(key, cancellationToken);
        await _mediator.Publish(new CombatInPreparationDeleted(combatInPreparation), cancellationToken);
    }

    static string GetKey(Guid guid)
    {
        return guid.ToString();
    }
}
