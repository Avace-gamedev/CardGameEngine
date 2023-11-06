using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Enchantments.Triggered.Instance;

public class TriggeredEffectInstance : IDisposable
{
    readonly Random _random;

    public TriggeredEffectInstance(TriggeredEffect effect, CharacterCombatState source, CharacterCombatState target, Random random)
    {
        _random = random;
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
    public bool HasExpired { get; private set; }

    public void Dispose()
    {
        TriggerState.Expired -= OnExpired;
        GC.SuppressFinalize(this);
    }

    public event EventHandler? Expired;

    void OnTriggered(object? _, EventArgs __)
    {
        Effect.Effect.Resolve(Source, new[] { Target }, _random);
    }

    void OnExpired(object? _, EventArgs __)
    {
        HasExpired = true;
        Expired?.Invoke(this, EventArgs.Empty);
    }
}
