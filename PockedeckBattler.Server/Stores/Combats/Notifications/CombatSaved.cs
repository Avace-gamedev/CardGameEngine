namespace PockedeckBattler.Server.Stores.Combats.Notifications;

public class CombatSaved : CombatNotification
{
    public CombatSaved(CombatWithMetadata combat) : base(combat)
    {
    }
}
