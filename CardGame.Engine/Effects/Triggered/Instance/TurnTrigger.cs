using CardGame.Engine.Combats;
using CardGame.Engine.Combats.State;

namespace CardGame.Engine.Effects.Triggered.Instance;

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

    public override TriggerState CreateNewState(CombatState combat, CharacterCombatState source, CharacterCombatState target)
    {
        return new State(combat, source, target, InitialDelay, Duration, Moment);
    }

    public class State : TriggerState
    {
        public State(
            CombatState combat,
            CharacterCombatState source,
            CharacterCombatState target,
            int triggersIn,
            int remainingDuration,
            TriggerMoment moment
        ) : base(combat, source, target)
        {
            TriggersIn = triggersIn;
            RemainingDuration = remainingDuration;
            Moment = moment;

            Combat.PhaseStarted += OnPhaseStarted;
        }

        public int TriggersIn { get; private set; }
        public int RemainingDuration { get; private set; }
        public TriggerMoment Moment { get; }

        void OnPhaseStarted(object? _, PhaseEventArgs args)
        {
            if (args.Side == Source.Side && args.Phase == CombatSideTurnPhase.StartOfTurn)
            {
                Tick();
            }

            if (TriggersIn <= 0 && RemainingDuration > 0)
            {
                bool shouldTrigger = false;
                switch (Moment)
                {
                    case TriggerMoment.StartOfSourceTurn:
                        shouldTrigger = args.Side == Source.Side && args.Phase == CombatSideTurnPhase.StartOfTurn;
                        break;
                    case TriggerMoment.EndOfSourceTurn:
                        shouldTrigger = args.Side == Source.Side && args.Phase == CombatSideTurnPhase.EndOfTurn;
                        break;
                    case TriggerMoment.StartOfTargetTurn:
                        shouldTrigger = args.Side == Target.Side && args.Phase == CombatSideTurnPhase.StartOfTurn;
                        break;
                    case TriggerMoment.EndOfTargetTurn:
                        shouldTrigger = args.Side == Target.Side && args.Phase == CombatSideTurnPhase.EndOfTurn;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (shouldTrigger)
                {
                    OnTriggered();
                }
            }
        }

        void Tick()
        {
            if (TriggersIn > 0)
            {
                TriggersIn--;
            }
            else
            {
                RemainingDuration--;
                if (RemainingDuration <= 0)
                {
                    OnExpired();
                }
            }
        }
    }
}
