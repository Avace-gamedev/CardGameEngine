using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Triggered;

namespace PockedeckBattler.Server.Views.Effects.Triggered;

public class TurnTriggerView : EffectTriggerView
{
    public TurnTriggerView(TurnMoment moment)
    {
        Moment = moment;
    }

    public TurnMoment Moment { get; }
}

public static class TurnTriggerViewMappingExtensions
{
    public static TurnTriggerView View(this TurnTrigger effect)
    {
        return new TurnTriggerView(effect.Moment);
    }
}
