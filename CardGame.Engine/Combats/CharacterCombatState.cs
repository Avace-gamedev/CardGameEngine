﻿using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Damages;
using CardGame.Engine.Combats.Modifiers;
using CardGame.Engine.Effects.Passive;
using CardGame.Engine.Effects.Passive.Stats;
using CardGame.Engine.Effects.Triggered;

namespace CardGame.Engine.Combats;

public class CharacterCombatState
{
    readonly List<PassiveEffectInstance> _passiveEffects = new();
    readonly List<TriggeredEffectInstance> _triggeredEffects = new();

    public CharacterCombatState(CombatSide side, Character character)
    {
        Side = side;
        Character = character;

        Health = Character.Stats.MaxHealth;
        Shield = 0;
        IsDead = false;
    }

    public CombatSide Side { get; }
    public Character Character { get; }
    public CharacterStatistics Stats => Character.Stats;

    public bool IsDead { get; private set; }
    public int Health { get; private set; }
    public int Shield { get; private set; }
    public IReadOnlyList<PassiveEffectInstance> PassiveEffects => _passiveEffects;
    public IReadOnlyList<TriggeredEffectInstance> TriggeredEffects => _triggeredEffects;

    public CharacterStatsModifier GetStatsModifier()
    {
        CharacterStatEffect[] characterStatEffects = PassiveEffects.Select(e => e.Effect).OfType<CharacterStatEffect>().ToArray();
        return characterStatEffects.Any()
            ? characterStatEffects.Select(e => e.GetStatsModifier()).Aggregate(CharacterStatsModifier.Combine)
            : CharacterStatsModifier.None;
    }

    public DamageReceived Damage(AttackDamage attack)
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

    public HealReceived Heal(int amount)
    {
        if (IsDead)
        {
            return new HealReceived(0);
        }

        int healthReceived = Math.Min(Stats.MaxHealth - Health, amount);

        SetHealth(Health + healthReceived);

        return new HealReceived(healthReceived);
    }

    public ShieldReceived AddShield(int amount)
    {
        if (IsDead)
        {
            return new ShieldReceived(0);
        }

        SetShield(Shield + amount);

        return new ShieldReceived(amount);
    }

    public void AddPassiveEffect(PassiveEffectInstance passiveEffect)
    {
        _passiveEffects.Add(passiveEffect);
    }

    public void AddTriggeredEffect(TriggeredEffectInstance triggeredEffect)
    {
        _triggeredEffects.Add(triggeredEffect);
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
}
