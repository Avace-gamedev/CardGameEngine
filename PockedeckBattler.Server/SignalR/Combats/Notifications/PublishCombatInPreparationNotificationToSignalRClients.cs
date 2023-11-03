using MediatR;
using Microsoft.AspNetCore.SignalR;
using PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;
using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats.Notifications;

public class PublishCombatInPreparationNotificationToSignalRClients
    : INotificationHandler<CombatInPreparationCreated>, INotificationHandler<CombatInPreparationSaved>, INotificationHandler<CombatInPreparationDeleted>,
        INotificationHandler<CombatInPreparationStarted>
{
    readonly IHubContext<CombatsHub, ICombatsHubClient> _hub;

    public PublishCombatInPreparationNotificationToSignalRClients(IHubContext<CombatsHub, ICombatsHubClient> hub)
    {
        _hub = hub;
    }

    public async Task Handle(CombatInPreparationCreated notification, CancellationToken cancellationToken)
    {
        await _hub.Clients.Group(notification.Combat.LeftPlayerName).CombatInPreparationCreated(notification.Combat.View());

        if (notification.Combat.RightPlayerName != null)
        {
            await _hub.Clients.Group(notification.Combat.RightPlayerName).CombatInPreparationCreated(notification.Combat.View());
        }
    }

    public async Task Handle(CombatInPreparationDeleted notification, CancellationToken cancellationToken)
    {
        await _hub.Clients.Group(notification.Combat.LeftPlayerName).CombatInPreparationDeleted(notification.Combat.View());

        if (notification.Combat.RightPlayerName != null)
        {
            await _hub.Clients.Group(notification.Combat.RightPlayerName).CombatInPreparationDeleted(notification.Combat.View());
        }
    }

    public async Task Handle(CombatInPreparationSaved notification, CancellationToken cancellationToken)
    {
        await _hub.Clients.Group(notification.Combat.LeftPlayerName).CombatInPreparationChanged(notification.Combat.View());

        if (notification.Combat.RightPlayerName != null)
        {
            await _hub.Clients.Group(notification.Combat.RightPlayerName).CombatInPreparationChanged(notification.Combat.View());
        }
    }

    public async Task Handle(CombatInPreparationStarted notification, CancellationToken cancellationToken)
    {
        await _hub.Clients.Group(notification.Combat.LeftPlayerName).CombatInPreparationStarted(notification.CombatId);

        if (notification.Combat.RightPlayerName != null)
        {
            await _hub.Clients.Group(notification.Combat.RightPlayerName).CombatInPreparationStarted(notification.CombatId);
        }
    }
}
