using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public class CombatStarted : CombatNotification
{
    public CombatStarted(CombatWithMetadata combat) : base(combat)
    {
    }
}
