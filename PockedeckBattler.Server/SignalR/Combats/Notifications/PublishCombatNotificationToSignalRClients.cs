using System.Collections.Concurrent;
using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Abstractions;
using Microsoft.AspNetCore.SignalR;
using PockedeckBattler.Server.BackgroundWorkers;
using PockedeckBattler.Server.Rest.Combats.Notifications;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats.Notifications;

public class PublishCombatNotificationToSignalRClients : PeriodicWorker
{
    readonly ConcurrentDictionary<string, DateTime> _lastPublishDate = new();
    readonly ILogger<PublishCombatNotificationToSignalRClients> _logger;
    readonly IServiceProvider _serviceProvider;
    readonly TimeSpan _throttlePeriod;
    readonly ConcurrentDictionary<string, CombatNotification> _toPublish = new();

    public PublishCombatNotificationToSignalRClients(IServiceProvider serviceProvider, TimeSpan throttlePeriod) : base(TimeSpan.FromSeconds(0.1))
    {
        _serviceProvider = serviceProvider;
        _throttlePeriod = throttlePeriod;

        _logger = serviceProvider.GetRequiredService<ILogger<PublishCombatNotificationToSignalRClients>>();
    }

    public void Publish(CombatNotification notification)
    {
        RegisterPublicationRequest(notification, notification.Combat.LeftPlayerName);
        RegisterPublicationRequest(notification, notification.Combat.RightPlayerName);
    }

    void RegisterPublicationRequest(CombatNotification notification, string playerName)
    {
        _toPublish[playerName] = notification;
    }

    protected override async Task Work(CancellationToken cancellationToken)
    {
        DateTime now = DateTime.Now;

        foreach (string playerName in _toPublish.Keys.ToArray())
        {
            DateTime lastPublishDate = _lastPublishDate.GetValueOrDefault(playerName);

            if (now <= lastPublishDate + _throttlePeriod)
            {
                continue;
            }

            if (_toPublish.Remove(playerName, out CombatNotification? notification))
            {
                await SendNotification(notification, playerName, cancellationToken);

                _lastPublishDate[playerName] = DateTime.Now;
            }
        }
    }

    async Task SendNotification(CombatNotification notification, string playerName, CancellationToken cancellationToken)
    {
        IHubContext<CombatsHub, ICombatsHubClient>? hub = _serviceProvider.GetService<IHubContext<CombatsHub, ICombatsHubClient>>();
        if (hub == null)
        {
            return;
        }

        CombatSide side = playerName == notification.Combat.LeftPlayerName
            ? CombatSide.Left
            : playerName == notification.Combat.RightPlayerName
                ? CombatSide.Right
                : CombatSide.None;

        if (side == CombatSide.None)
        {
            _logger.LogWarning(
                "Was trying to publish message to player {playerName} not part of combat, combat players are: {leftName} and {rightName}",
                playerName,
                notification.Combat.LeftPlayerName,
                notification.Combat.RightPlayerName
            );
            return;
        }

        switch (notification.Event)
        {
            case CombatEvent.Created:
                await hub.Clients.Group(playerName).CombatCreated(notification.Combat.PlayerView(side));
                break;
            case CombatEvent.Ended:
                await hub.Clients.Group(playerName).CombatEnded(notification.Combat.PlayerView(side));
                break;
            case CombatEvent.Updated:
            default:
                await hub.Clients.Group(playerName).CombatUpdated(notification.Combat.PlayerView(side));
                break;
        }
    }
}
