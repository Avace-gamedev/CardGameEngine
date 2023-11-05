using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Triggered.Instance;

public class TriggeredEffectInstance : IDisposable
{
    public TriggeredEffectInstance(TriggeredEffect effect, CharacterCombatState source, CharacterCombatState target)
    {
        Id = Guid.NewGuid();
        Effect = effect;
        Source = source;
        Target = target;
        TriggerState = effect.Trigger.CreateNewState(source.Combat, Source, Target);

        TriggerState.Triggered += OnTriggered;
        TriggerState.Expired += OnExpired;
    }

    public Guid Id { get; }

    public TriggeredEffect Effect { get; }

    public CharacterCombatState Source { get; }
    public CharacterCombatState Target { get; }

    public TriggerState TriggerState { get; }

    public void Dispose()
    {
        TriggerState.Expired -= OnExpired;
        GC.SuppressFinalize(this);
    }

    public event EventHandler? Expired;

    void OnTriggered(object? _, EventArgs __)
    {
        Effect.Effect.Resolve(Source, new[] { Target });
    }

    void OnExpired(object? _, EventArgs __)
    {
        Expired?.Invoke(this, EventArgs.Empty);
    }
}
