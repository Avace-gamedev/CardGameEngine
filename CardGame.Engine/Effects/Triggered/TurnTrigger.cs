using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Triggered;

public class TurnTrigger : EffectTrigger
{
    public TurnTrigger(TurnMoment moment)
    {
        Moment = moment;
    }

    public TurnMoment Moment { get; }
}
