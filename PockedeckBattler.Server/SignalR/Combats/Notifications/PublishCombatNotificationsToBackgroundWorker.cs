using MediatR;
using PockedeckBattler.Server.Rest.Combats.Notifications;

namespace PockedeckBattler.Server.SignalR.Combats.Notifications;

public class PublishCombatNotificationsToBackgroundWorker : INotificationHandler<CombatNotification>
{
    readonly PublishCombatNotificationToSignalRClients _worker;

    public PublishCombatNotificationsToBackgroundWorker(PublishCombatNotificationToSignalRClients worker)
    {
        _worker = worker;
    }

    public Task Handle(CombatNotification notification, CancellationToken cancellationToken)
    {
        _worker.Publish(notification);

        return Task.CompletedTask;
    }
}
