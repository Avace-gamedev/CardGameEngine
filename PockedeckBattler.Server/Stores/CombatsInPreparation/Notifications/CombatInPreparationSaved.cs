namespace PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

public class CombatInPreparationSaved : CombatInPreparationNotification
{
    public CombatInPreparationSaved(StoredCombatInPreparation combat) : base(combat)
    {
    }
}
