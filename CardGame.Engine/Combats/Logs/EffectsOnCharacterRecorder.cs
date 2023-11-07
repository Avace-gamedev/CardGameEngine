using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects.Enchantments;

namespace CardGame.Engine.Combats.Logs;

class EffectsOnCharacterRecorder : IDisposable
{
    readonly Action<IEnumerable<EffectOnCharacterLogEntry>> _callback;
    readonly List<EffectOnCharacterLogEntry> _effects = new();
    readonly CharacterCombatState? _leftBack;
    readonly CharacterCombatState? _leftFront;
    readonly CharacterCombatState? _rightBack;
    readonly CharacterCombatState? _rightFront;

    public EffectsOnCharacterRecorder(CombatState combat, Action<IEnumerable<EffectOnCharacterLogEntry>> callback)
    {
        _callback = callback;

        _leftFront = combat.LeftSide.Front;
        if (_leftFront != null)
        {
            RegisterCallbacks(_leftFront);
        }

        _leftBack = combat.LeftSide.Back;
        if (_leftBack != null)
        {
            RegisterCallbacks(_leftBack);
        }

        _rightFront = combat.RightSide.Front;
        if (_rightFront != null)
        {
            RegisterCallbacks(_rightFront);
        }

        _rightBack = combat.RightSide.Back;
        if (_rightBack != null)
        {
            RegisterCallbacks(_rightBack);
        }
    }

    public void Dispose()
    {
        if (_leftFront != null)
        {
            UnregisterCallbacks(_leftFront);
        }

        if (_leftBack != null)
        {
            UnregisterCallbacks(_leftBack);
        }

        if (_rightFront != null)
        {
            UnregisterCallbacks(_rightFront);
        }

        if (_rightBack != null)
        {
            UnregisterCallbacks(_rightBack);
        }

        _callback(_effects);

        GC.SuppressFinalize(this);
    }

    void RegisterCallbacks(CharacterCombatState character)
    {
        character.DamageReceived += OnDamageReceived;
        character.HealReceived += OnHealReceived;
        character.ShieldReceived += OnShieldReceived;
        character.EnchantmentAdded += OnEnchantmentAdded;
    }

    void UnregisterCallbacks(CharacterCombatState character)
    {
        character.DamageReceived -= OnDamageReceived;
        character.HealReceived -= OnHealReceived;
        character.ShieldReceived -= OnShieldReceived;
        character.EnchantmentAdded -= OnEnchantmentAdded;
    }

    void OnDamageReceived(object? sender, DamageReceived e)
    {
        if (sender is not CharacterCombatState target)
        {
            return;
        }

        RecordEffectEntry(new DamageEffectOnCharacterLogEntry(new CharacterLogEntry(target.Character.Identity.Name, target.Side), e));
    }

    void OnHealReceived(object? sender, HealReceived e)
    {
        if (sender is not CharacterCombatState target)
        {
            return;
        }

        RecordEffectEntry(new HealEffectOnCharacterLogEntry(new CharacterLogEntry(target.Character.Identity.Name, target.Side), e));
    }

    void OnShieldReceived(object? sender, ShieldReceived e)
    {
        if (sender is not CharacterCombatState target)
        {
            return;
        }

        RecordEffectEntry(new ShieldEffectOnCharacterLogEntry(new CharacterLogEntry(target.Character.Identity.Name, target.Side), e));
    }

    void OnEnchantmentAdded(object? sender, Enchantment e)
    {
        if (sender is not CharacterCombatState target)
        {
            return;
        }

        RecordEffectEntry(new AddEnchantmentEffectOnCharacterLogEntry(new CharacterLogEntry(target.Character.Identity.Name, target.Side), e));
    }

    void RecordEffectEntry(EffectOnCharacterLogEntry entry)
    {
        _effects.Add(entry);
    }
}
