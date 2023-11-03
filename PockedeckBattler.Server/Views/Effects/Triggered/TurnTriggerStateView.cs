using CardGame.Engine.Effects.Triggered;

namespace PockedeckBattler.Server.Views.Effects.Triggered;

public class TurnTriggerStateView : TriggerStateView
{
    public TurnTriggerStateView(int triggersIn, int remainingDuration)
    {
        TriggersIn = triggersIn;
        RemainingDuration = remainingDuration;
    }

    public int TriggersIn { get; }
    public int RemainingDuration { get; }
}

public static class TurnTriggerStateViewMappingExtensions
{
    public static TurnTriggerStateView View(this TurnTrigger.State state)
    {
        return new TurnTriggerStateView(state.TriggersIn, state.RemainingDuration);
    }
}
