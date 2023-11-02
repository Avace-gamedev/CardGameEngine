using MediatR;
using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public abstract class CombatNotification : INotification
{
    protected CombatNotification(CombatWithMetadata combat)
    {
        Combat = combat;
    }

    public CombatWithMetadata Combat { get; }
}
