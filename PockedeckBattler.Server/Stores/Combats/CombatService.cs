using System.Runtime.CompilerServices;
using MediatR;
using PockedeckBattler.Server.Stores.Combats.Notifications;

namespace PockedeckBattler.Server.Stores.Combats;

public class CombatService : ICombatService
{
    readonly IMediator _mediator;
    readonly IStore<CombatWithMetadata> _store;

    public CombatService(IStore<CombatWithMetadata> store, IMediator mediator)
    {
        _store = store;
        _mediator = mediator;
    }

    public async Task SaveCombat(CombatWithMetadata combat, CancellationToken cancellationToken = default)
    {
        string key = GetKey(combat.Id);
        bool created = !await _store.Exists(key, cancellationToken);

        await _store.Save(key, combat, cancellationToken);

        if (created)
        {
            await _mediator.Publish(new CombatCreated(combat), cancellationToken);
        }
        else
        {
            await _mediator.Publish(new CombatSaved(combat), cancellationToken);
        }
    }

    public async Task<CombatWithMetadata?> GetCombat(Guid id, CancellationToken cancellationToken = default)
    {
        return await _store.Load(GetKey(id), cancellationToken);
    }

    public async IAsyncEnumerable<CombatWithMetadata> GetCombatsInvolvingPlayer(
        string player,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        await foreach (CombatWithMetadata combat in _store.LoadAll(cancellationToken))
        {
            if (combat.LeftPlayerName == player || combat.RightPlayerName == player)
            {
                yield return combat;
            }
        }
    }

    static string GetKey(Guid id)
    {
        return id.ToString();
    }
}
