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

        return damageReceived;
    }

    public HealReceived Heal(int amount)
    {
        HealReceived healReceived = ComputeHealReceived(amount);

        HealReceived?.Invoke(this, healReceived);

        return healReceived;
    }

    public ShieldReceived AddShield(int amount)
    {
        ShieldReceived shieldReceived = ComputeShieldReceived(amount);

        ShieldReceived?.Invoke(this, shieldReceived);

        return shieldReceived;
    }

    public void AddEnchantment(Enchantment enchantment, CharacterCombatState source)
    {
        EnchantmentInstance enchantmentInstance = new(enchantment, source, this, _random);
        enchantmentInstance.Expired += OnEffectExpired;
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
            IsDead = true;
        }
    }

    void OnEffectExpired(object? sender, EventArgs _)
    {
        EnchantmentInstance[] toRemove = _enchantments.Where(e => e.HasExpired).ToArray();

        foreach (EnchantmentInstance enchantment in toRemove)
        {
            _enchantments.Remove(enchantment);
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

            SetShield(0);

            int healthDamage = modifiedAttack.Amount - shieldDamage;

            if (healthDamage > Health)
            {
                healthDamage = Health;
            }

            SetHealth(Health - healthDamage);

            return new DamageReceived(healthDamage, shieldDamage);
        }

        SetShield(Shield - modifiedAttack.Amount);

        return new DamageReceived(0, modifiedAttack.Amount);
    }

    HealReceived ComputeHealReceived(int amount)
    {
        if (IsDead)
        {
            return new HealReceived(0);
        }

        int healthReceived = Math.Min(Stats.MaxHealth - Health, amount);

        SetHealth(Health + healthReceived);

        return new HealReceived(healthReceived);
    }

    ShieldReceived ComputeShieldReceived(int amount)
    {

        if (IsDead)
        {
            return new ShieldReceived(0);
        }

        SetShield(Shield + amount);

        return new ShieldReceived(amount);
    }
}
