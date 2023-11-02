using MediatR;

namespace PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

public abstract class CombatInPreparationNotification : INotification
{
    protected CombatInPreparationNotification(StoredCombatInPreparation combat)
    {
        Combat = combat;
    }

    public StoredCombatInPreparation Combat { get; }
}
