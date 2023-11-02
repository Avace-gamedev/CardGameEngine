using MediatR;
using Microsoft.AspNetCore.SignalR;
using PockedeckBattler.Server.Stores.CombatsInPreparation;
using PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats.Notifications;

public class PublishCombatInPreparationNotificationToSignalRClients
    : INotificationHandler<CombatInPreparationCreated>, INotificationHandler<CombatInPreparationSaved>, INotificationHandler<CombatInPreparationDeleted>
{
    readonly IHubConnections _connections;
    readonly IHubContext<CombatsHub, ICombatsHubClient> _hub;

    public PublishCombatInPreparationNotificationToSignalRClients(IHubConnections connections, IHubContext<CombatsHub, ICombatsHubClient> hub)
    {
        _connections = connections;
        _hub = hub;
    }

    public async Task Handle(CombatInPreparationCreated notification, CancellationToken cancellationToken)
    {
        if (!HasTargets(notification.Combat, out IEnumerable<string> connectionIds))
        {
            return;
        }

        foreach (string id in connectionIds)
        {
            await _hub.Clients.Client(id).CombatInPreparationCreated(notification.Combat.View());
        }
    }

    public async Task Handle(CombatInPreparationDeleted notification, CancellationToken cancellationToken)
    {
        if (!HasTargets(notification.Combat, out IEnumerable<string> connectionIds))
        {
            return;
        }

        foreach (string id in connectionIds)
        {
            await _hub.Clients.Client(id).CombatInPreparationDeleted(notification.Combat.View());
        }
    }

    public async Task Handle(CombatInPreparationSaved notification, CancellationToken cancellationToken)
    {
        if (!HasTargets(notification.Combat, out IEnumerable<string> connectionIds))
        {
            return;
        }

        foreach (string id in connectionIds)
        {
            await _hub.Clients.Client(id).CombatInPreparationChanged(notification.Combat.View());
        }
    }

    bool HasTargets(StoredCombatInPreparation combat, out IEnumerable<string> connectionIds)
    {
        IEnumerable<string> players = new[] { combat.LeftPlayerName, combat.RightPlayerName }.Where(name => name != null).Select(name => name!);
        connectionIds = players.Select(name => _connections.GetConnection(name)).Where(name => name != null).Select(name => name!).ToArray();

        if (!connectionIds.Any())
        {
            return false;
        }

        return true;
    }
}
