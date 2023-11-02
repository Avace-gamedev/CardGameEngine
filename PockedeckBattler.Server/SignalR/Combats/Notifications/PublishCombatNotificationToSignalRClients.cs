using System.Diagnostics.CodeAnalysis;
using CardGame.Engine.Combats;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using PockedeckBattler.Server.Stores.Combats.Notifications;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats.Notifications;

public class PublishCombatNotificationToSignalRClients : INotificationHandler<CombatCreated>, INotificationHandler<CombatSaved>
{
    readonly IHubConnections _connections;
    readonly IHubContext<CombatsHub, ICombatsHubClient> _hub;

    public PublishCombatNotificationToSignalRClients(IHubConnections connections, IHubContext<CombatsHub, ICombatsHubClient> hub)
    {
        _connections = connections;
        _hub = hub;
    }

    public async Task Handle(CombatCreated notification, CancellationToken cancellationToken)
    {
        if (IsConnected(notification.Combat.LeftPlayerName, out string? leftId))
        {
            await _hub.Clients.Client(leftId).CombatCreated(notification.Combat.PlayerView(CombatSide.Left));
        }

        if (IsConnected(notification.Combat.RightPlayerName, out string? rightId))
        {
            await _hub.Clients.Client(rightId).CombatCreated(notification.Combat.PlayerView(CombatSide.Right));
        }
    }

    public async Task Handle(CombatSaved notification, CancellationToken cancellationToken)
    {
        if (IsConnected(notification.Combat.LeftPlayerName, out string? leftId))
        {
            await _hub.Clients.Client(leftId).CombatChanged(notification.Combat.PlayerView(CombatSide.Left));
        }

        if (IsConnected(notification.Combat.RightPlayerName, out string? rightId))
        {
            await _hub.Clients.Client(rightId).CombatChanged(notification.Combat.PlayerView(CombatSide.Right));
        }
    }

    bool IsConnected(string name, [NotNullWhen(true)] out string? connectionId)
    {
        connectionId = _connections.GetConnection(name);
        return connectionId != null;
    }
}
