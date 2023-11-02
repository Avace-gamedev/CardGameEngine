namespace PockedeckBattler.Server.Stores.Combats.Notifications;

public class CombatCreated : CombatNotification
{
    public CombatCreated(CombatWithMetadata combat) : base(combat)
    {
    }
}
