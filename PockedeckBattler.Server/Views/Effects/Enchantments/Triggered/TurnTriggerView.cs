using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace PockedeckBattler.Server.Views.Effects.Enchantments.Triggered;

public class TurnTriggerView : EffectTriggerView
{
    public TurnTriggerView(TurnTrigger.TriggerMoment moment, int duration, int initialDelay)
    {
        Moment = moment;
        Duration = duration;
        InitialDelay = initialDelay;
    }

    public TurnTrigger.TriggerMoment Moment { get; }
    public int Duration { get; }
    public int InitialDelay { get; }
}

public static class TurnTriggerViewMappingExtensions
{
    public static TurnTriggerView View(this TurnTrigger effect)
    {
        return new TurnTriggerView(effect.Moment, effect.Duration, effect.InitialDelay);
    }
}
