using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public class CombatCreated : CombatNotification
{
    public CombatCreated(CombatWithMetadata combat) : base(combat)
    {
    }
}
