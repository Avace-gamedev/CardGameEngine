using CardGameEngine.Combats;

namespace CardGameEngine.Effects.Triggered;

public class TurnTrigger : EffectTrigger
{
    public TurnTrigger(TurnMoment moment)
    {
        Moment = moment;
    }

    TurnMoment Moment { get; }
}
