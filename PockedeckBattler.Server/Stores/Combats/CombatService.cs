using System.Runtime.CompilerServices;
using MediatR;

namespace PockedeckBattler.Server.Stores.Combats;

public class CombatService : ICombatService
{
    readonly IMediator _mediator;
    readonly IStore<Combat> _store;

    public CombatService(IStore<Combat> store, IMediator mediator)
    {
        _store = store;
        _mediator = mediator;
    }

    public async Task SaveCombat(Combat combat, CancellationToken cancellationToken = default)
    {
        await _store.Save(GetKey(combat.Id), combat, cancellationToken);
    }

    public async Task<Combat?> GetCombat(Guid id, CancellationToken cancellationToken = default)
    {
        return await _store.Load(GetKey(id), cancellationToken);
    }

    public async IAsyncEnumerable<Combat> GetCombatsInvolvingPlayer(string player, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (Combat combat in _store.LoadAll(cancellationToken))
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
