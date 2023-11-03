using System.Diagnostics.CodeAnalysis;
using CardGame.Engine.Combats;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using PockedeckBattler.Server.Rest.Combats.Notifications;
using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats.Notifications;

public class PublishCombatNotificationToSignalRClients : INotificationHandler<CombatNotification>
{
    readonly IHubConnections _connections;
    readonly IHubContext<CombatsHub, ICombatsHubClient> _hub;

    public PublishCombatNotificationToSignalRClients(IHubConnections connections, IHubContext<CombatsHub, ICombatsHubClient> hub)
    {
        _connections = connections;
        _hub = hub;
    }

    public async Task Handle(CombatNotification notification, CancellationToken cancellationToken)
    {
        Func<ICombatsHubClient, CombatSide, Task> action = notification.Event switch
        {
            CombatEvent.Created => (hubClient, side) => hubClient.CombatCreated(notification.Combat.PlayerView(side)),
            CombatEvent.Ended => (hubClient, side) => hubClient.CombatEnded(notification.Combat.PlayerView(side)),
            _ => (hubClient, side) => hubClient.CombatUpdated(notification.Combat.PlayerView(side))
        };

        await Notify(notification.Combat, action, cancellationToken);
    }

    public async Task Notify(CombatWithMetadata combat, Func<ICombatsHubClient, CombatSide, Task> notify, CancellationToken cancellationToken)
    {
        if (IsConnected(combat.LeftPlayerName, out string? leftId))
        {
            await notify(_hub.Clients.Client(leftId), CombatSide.Left);

        }

        if (IsConnected(combat.RightPlayerName, out string? rightId))
        {
            await notify(_hub.Clients.Client(rightId), CombatSide.Right);
        }
    }

    bool IsConnected(string name, [NotNullWhen(true)] out string? connectionId)
    {
        connectionId = _connections.GetConnection(name);
        return connectionId != null;
    }
}
