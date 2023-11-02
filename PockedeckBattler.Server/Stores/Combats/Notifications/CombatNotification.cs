using MediatR;

namespace PockedeckBattler.Server.Stores.Combats.Notifications;

public abstract class CombatNotification : INotification
{
    protected CombatNotification(CombatWithMetadata combat)
    {
        Combat = combat;
    }

    public CombatWithMetadata Combat { get; }
}
