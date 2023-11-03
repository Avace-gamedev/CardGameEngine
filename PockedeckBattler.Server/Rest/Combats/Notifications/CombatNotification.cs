using MediatR;
using PockedeckBattler.Server.Stores.Combats;

namespace PockedeckBattler.Server.Rest.Combats.Notifications;

public class CombatNotification : INotification
{
    public CombatNotification(CombatWithMetadata combat, CombatEvent @event)
    {
        Combat = combat;
        Event = @event;
    }

    public CombatWithMetadata Combat { get; }
    public CombatEvent Event { get; }
}

public enum CombatEvent
{
    Created,
    TurnStarted,
    PhaseStarted,
    Ended
}
