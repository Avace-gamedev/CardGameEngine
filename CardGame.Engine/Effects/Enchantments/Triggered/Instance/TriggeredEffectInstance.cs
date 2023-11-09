using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects.Enchantments.Triggered.Instance;

public class TriggeredEffectInstance : IDisposable
{
    readonly Random _random;

    public TriggeredEffectInstance(TriggeredEffect effect, EnchantmentInstance enchantment, Random random)
    {
        _random = random;
        Id = Guid.NewGuid();
        Effect = effect;
        Enchantment = enchantment;
        TriggerState = effect.Trigger.CreateNewState(enchantment.Source.Combat, enchantment.Source, enchantment.Target);

        TriggerState.Triggered += OnTriggered;
        TriggerState.Expired += OnExpired;
    }

    public Guid Id { get; }

    public TriggeredEffect Effect { get; }

    public EnchantmentInstance Enchantment { get; }
    public CharacterCombatState Source => Enchantment.Source;
    public CharacterCombatState Target => Enchantment.Target;

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
        CombatState combat = Enchantment.Source.Combat;
        using (combat.Log.RecordTriggeredEffect(this))
        {
            Effect.Effect.Resolve(Enchantment.Source, new[] { Enchantment.Target }, _random);
        }
    }

    void OnExpired(object? _, EventArgs __)
    {
        HasExpired = true;
        Expired?.Invoke(this, EventArgs.Empty);
    }
}
