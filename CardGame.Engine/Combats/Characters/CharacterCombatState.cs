﻿using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Modifiers;
using CardGame.Engine.Effects.Enchantments;
using CardGame.Engine.Effects.Enchantments.Passive.Stats;

namespace CardGame.Engine.Combats.Characters;

public class CharacterCombatState
{
    readonly List<EnchantmentInstance> _enchantments = new();
    readonly Random _random;

    public CharacterCombatState(CombatState combat, CombatSide side, Character character, Random random)
    {
        _random = random;
        Combat = combat;
        Side = side;
        Character = character;

        Health = Character.Stats.MaxHealth;
        Shield = 0;
        IsDead = false;
    }

    public CombatState Combat { get; }
    public CombatSide Side { get; }
    public Character Character { get; }
    public CharacterStatistics Stats => Character.Stats;

    public bool IsDead { get; private set; }
    public int Health { get; private set; }
    public int Shield { get; private set; }
    public IReadOnlyList<EnchantmentInstance> Enchantments => _enchantments;

    public event EventHandler<DamageReceived>? DamageReceived;
    public event EventHandler<HealReceived>? HealReceived;
    public event EventHandler<ShieldReceived>? ShieldReceived;
    public event EventHandler<Enchantment>? EnchantmentAdded;
    public event EventHandler? Died;

    public CharacterStatsModifier GetStatsModifier()
    {
        ChangeCharacterStatEffect[] characterStatEffects = Enchantments.SelectMany(e => e.Enchantment.Passive).OfType<ChangeCharacterStatEffect>().ToArray();
        return characterStatEffects.Any()
            ? characterStatEffects.Select(e => e.GetStatsModifier()).Aggregate(CharacterStatsModifier.Combine)
            : CharacterStatsModifier.None;
    }

    public DamageReceived Damage(AttackDamage attack)
    {
        DamageReceived damageReceived = ComputeDamageReceived(attack);

        DamageReceived?.Invoke(this, damageReceived);

        SetShield(Shield - damageReceived.Shield);
        SetHealth(Health - damageReceived.Health);

        return damageReceived;
    }

    public HealReceived Heal(int amount)
    {
        HealReceived healReceived = ComputeHealReceived(amount);

        HealReceived?.Invoke(this, healReceived);

        SetHealth(Health + healReceived.Health);

        return healReceived;
    }

    public ShieldReceived AddShield(int amount)
    {
        ShieldReceived shieldReceived = ComputeShieldReceived(amount);

        ShieldReceived?.Invoke(this, shieldReceived);

        SetShield(Shield + shieldReceived.Shield);

        return shieldReceived;
    }

    public void AddEnchantment(Enchantment enchantment, CharacterCombatState source)
    {
        EnchantmentInstance enchantmentInstance = new(enchantment, source, this, _random);
        enchantmentInstance.Expired += OnEnchantmentExpired;
        _enchantments.Add(enchantmentInstance);

        EnchantmentAdded?.Invoke(this, enchantment);
    }

    void SetShield(int shield)
    {
        if (IsDead)
        {
            return;
        }

        Shield = Math.Max(0, shield);
    }

    void SetHealth(int health)
    {
        if (IsDead)
        {
            return;
        }

        Health = Math.Max(0, Math.Min(health, Stats.MaxHealth));

        if (Health == 0)
        {
            Die();
        }
    }

    void OnEnchantmentExpired(object? sender, EventArgs _)
    {
        EnchantmentInstance[] toRemove = _enchantments.Where(e => e.HasExpired).ToArray();

        foreach (EnchantmentInstance enchantment in toRemove)
        {
            _enchantments.Remove(enchantment);
            enchantment.Expired -= OnEnchantmentExpired;
            enchantment.Dispose();
        }
    }

    DamageReceived ComputeDamageReceived(AttackDamage attack)
    {
        if (IsDead || attack.Amount <= 0)
        {
            return new DamageReceived(0, 0);
        }

        CharacterStatsModifier modifiers = GetStatsModifier();
        AttackDamage modifiedAttack = modifiers.ModifyReceivedAttack(attack);

        if (modifiedAttack.Amount > Shield)
        {
            int shieldDamage = Shield;

            int healthDamage = modifiedAttack.Amount - shieldDamage;

            if (healthDamage > Health)
            {
                healthDamage = Health;
            }

            return new DamageReceived(healthDamage, shieldDamage);
        }

        return new DamageReceived(0, modifiedAttack.Amount);
    }

    HealReceived ComputeHealReceived(int amount)
    {
        if (IsDead)
        {
            return new HealReceived(0);
        }

        int healthReceived = Math.Min(Stats.MaxHealth - Health, amount);

        return new HealReceived(healthReceived);
    }

    ShieldReceived ComputeShieldReceived(int amount)
    {
        if (IsDead)
        {
            return new ShieldReceived(0);
        }

        return new ShieldReceived(amount);
    }

    void Die()
    {
        if (IsDead)
        {
            return;
        }

        Health = 0;
        Shield = 0;
        IsDead = true;

        foreach (EnchantmentInstance enchantment in _enchantments)
        {
            enchantment.Expired -= OnEnchantmentExpired;
            enchantment.Dispose();
        }
        _enchantments.Clear();

        Died?.Invoke(this, EventArgs.Empty);
    }
}
