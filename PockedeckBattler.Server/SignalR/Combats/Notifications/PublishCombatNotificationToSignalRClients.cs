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

        Func<ICombatsHubClient, Task> action = notification.Event switch
        {
            CombatEvent.Created => hubClient => hubClient.CombatCreated(notification.Combat.PlayerView(CombatSide.Left)),
            CombatEvent.TurnStarted => hubClient => hubClient.CombatTurnStarted(notification.Combat.PlayerView(CombatSide.Left)),
            CombatEvent.PhaseStarted => hubClient => hubClient.CombatPhaseStarted(notification.Combat.PlayerView(CombatSide.Left)),
            CombatEvent.Ended => hubClient => hubClient.CombatEnded(notification.Combat.PlayerView(CombatSide.Left)),
            _ => throw new ArgumentOutOfRangeException()
        };

        await Notify(notification.Combat, action, cancellationToken);
    }

    public async Task Notify(CombatWithMetadata combat, Func<ICombatsHubClient, Task> notify, CancellationToken cancellationToken)
    {
        if (IsConnected(combat.LeftPlayerName, out string? leftId))
        {
            await notify(_hub.Clients.Client(leftId));

        }

        if (IsConnected(combat.RightPlayerName, out string? rightId))
        {
            await notify(_hub.Clients.Client(rightId));
        }
    }

    bool IsConnected(string name, [NotNullWhen(true)] out string? connectionId)
    {
        connectionId = _connections.GetConnection(name);
        return connectionId != null;
    }
}
