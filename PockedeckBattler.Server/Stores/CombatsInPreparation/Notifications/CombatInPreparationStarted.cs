namespace PockedeckBattler.Server.Stores.CombatsInPreparation.Notifications;

public class CombatInPreparationStarted : CombatInPreparationNotification
{
    public CombatInPreparationStarted(CombatInPreparation combat, Guid combatId) : base(combat)
    {
        CombatId = combatId;
    }

    public Guid CombatId { get; }
}
