namespace PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

public class CombatInPreparationCreated : CombatInPreparationNotification
{
    public CombatInPreparationCreated(StoredCombatInPreparation combat) : base(combat)
    {
    }
}
