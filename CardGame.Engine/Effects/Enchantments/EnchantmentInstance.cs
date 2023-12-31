﻿using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects.Enchantments.Passive;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace CardGame.Engine.Effects.Enchantments;

public class EnchantmentInstance : IDisposable
{
    public EnchantmentInstance(Enchantment enchantment, CharacterCombatState source, CharacterCombatState target, Random random)
    {
        Id = Guid.NewGuid();
        Enchantment = enchantment;
        Source = source;
        Target = target;
        PassiveEffects = enchantment.Passive.Select(p => new PassiveEffectInstance(p, source, target)).ToArray();
        TriggeredEffects = enchantment.Triggered.Select(t => new TriggeredEffectInstance(t, this, random)).ToArray();

        foreach (PassiveEffectInstance p in PassiveEffects)
        {
            p.Expired += OnEffectExpired;
        }

        foreach (TriggeredEffectInstance t in TriggeredEffects)
        {
            t.Expired += OnEffectExpired;
        }

        Source.Died += OnSourceOrTargetDied;
        Target.Died += OnSourceOrTargetDied;
    }

    public Guid Id { get; }

    public Enchantment Enchantment { get; }

    public CharacterCombatState Source { get; }

    public CharacterCombatState Target { get; }

    public IReadOnlyList<PassiveEffectInstance> PassiveEffects { get; }

    public IReadOnlyList<TriggeredEffectInstance> TriggeredEffects { get; }

    public bool HasExpired { get; private set; }

    public void Dispose()
    {
        foreach (PassiveEffectInstance passive in PassiveEffects)
        {
            passive.Dispose();
        }

        foreach (TriggeredEffectInstance triggered in TriggeredEffects)
        {
            triggered.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    public event EventHandler? Expired;

    void OnEffectExpired(object? _, EventArgs __)
    {
        if (PassiveEffects.All(p => p.HasExpired) && TriggeredEffects.All(t => t.HasExpired))
        {
            Expire();
        }
    }

    void OnSourceOrTargetDied(object? sender, EventArgs e)
    {
        Expire();
    }

    void Expire()
    {
        if (HasExpired)
        {
            return;
        }

        HasExpired = true;
        Source.Combat.Log.RecordEnchantmentExpired(this);
        Expired?.Invoke(this, EventArgs.Empty);
    }
}
