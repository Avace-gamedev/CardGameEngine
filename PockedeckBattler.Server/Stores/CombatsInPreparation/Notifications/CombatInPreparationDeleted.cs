namespace PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

public class CombatInPreparationDeleted : CombatInPreparationNotification
{
    public CombatInPreparationDeleted(StoredCombatInPreparation combat) : base(combat)
    {
    }
}
