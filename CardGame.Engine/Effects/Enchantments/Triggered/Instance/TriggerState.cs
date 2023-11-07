using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects.Enchantments.Triggered.Instance;

public abstract class TriggerState : IDisposable
{
    protected TriggerState(CombatState combat, CharacterCombatState source, CharacterCombatState target)
    {
        Combat = combat;
        Source = source;
        Target = target;
    }

    public CombatState Combat { get; }
    public CharacterCombatState Source { get; }
    public CharacterCombatState Target { get; }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public event EventHandler? Triggered;
    public event EventHandler? Expired;

    protected void OnExpired()
    {
        Expired?.Invoke(this, EventArgs.Empty);
    }

    protected void OnTriggered()
    {
        Triggered?.Invoke(this, EventArgs.Empty);
    }
}
