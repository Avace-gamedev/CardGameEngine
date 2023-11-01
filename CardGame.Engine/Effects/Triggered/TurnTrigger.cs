namespace CardGame.Engine.Effects.Triggered;

public class TurnTrigger : EffectTrigger
{
    public enum TriggerMoment
    {
        StartOfSourceTurn,
        EndOfSourceTurn,
        StartOfTargetTurn,
        EndOfTargetTurn
    }

    public TurnTrigger(TriggerMoment moment, int duration, int initialDelay = 0)
    {
        Moment = moment;
        Duration = duration;
        InitialDelay = initialDelay;
    }

    public TriggerMoment Moment { get; }

    public int Duration { get; }

    public int InitialDelay { get; }

    public override State CreateNewState()
    {
        return new State(InitialDelay, Duration);
    }

    public class State : TriggerState
    {
        public State(int triggersIn, int remainingDuration)
        {
            TriggersIn = triggersIn;
            RemainingDuration = remainingDuration;
        }

        public int TriggersIn { get; }
        public int RemainingDuration { get; }
    }
}
