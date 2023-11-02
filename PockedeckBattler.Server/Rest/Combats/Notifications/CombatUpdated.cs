using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public class CombatUpdated : CombatNotification
{
    public CombatUpdated(CombatWithMetadata combat) : base(combat)
    {
    }
}
