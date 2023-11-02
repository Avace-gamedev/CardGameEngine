using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public class CombatOver : CombatNotification
{
    public CombatOver(CombatWithMetadata combat) : base(combat)
    {
    }
}
