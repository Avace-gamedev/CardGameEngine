using MediatR;

namespace PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

public abstract class CombatInPreparationNotification : INotification
{
    protected CombatInPreparationNotification(CombatInPreparation combat)
    {
        Combat = combat;
    }

    public CombatInPreparation Combat { get; }
}
