using MediatR;
using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public class CombatNotification : INotification
{
    public CombatNotification(CombatInstanceWithMetadata combat, CombatEvent @event)
    {
        Combat = combat;
        Event = @event;
    }

    public CombatInstanceWithMetadata Combat { get; }
    public CombatEvent Event { get; }
}

public enum CombatEvent
{
    Created,
    TurnStarted,
    PhaseStarted,
    Ended
}
