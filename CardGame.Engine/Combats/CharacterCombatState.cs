using CardGame.Engine.Characters;
using CardGame.Engine.Effects.Passive;
using CardGame.Engine.Effects.Triggered;

namespace CardGame.Engine.Combats;

public class CharacterCombatState
{
    readonly List<PassiveEffectInstance> _passiveEffects = new();
    readonly List<TriggeredEffectInstance> _triggeredEffects = new();

    public CharacterCombatState(CombatInstance combat, CombatSide side, Character character)
    {
        Combat = combat;
        Side = side;
        Character = character;

        Health = Character.Stats.MaxHealth;
        Shield = 0;
        IsDead = false;
    }

    public CombatInstance Combat { get; }
    public CombatSide Side { get; }
    public Character Character { get; }
    public CharacterStatistics Stats => Character.Stats;

    public StatsModifier StatsModifier =>
        PassiveEffects.Select(e => e.Effect).OfType<PassiveStatsModifier>().Any()
            ? PassiveEffects.Select(e => e.Effect).OfType<PassiveStatsModifier>().Select(e => e.StatsModifier).Aggregate(StatsModifier.Combine)
            : StatsModifier.None;

    public bool IsDead { get; private set; }
    public int Health { get; private set; }
    public int Shield { get; private set; }
    public IReadOnlyList<PassiveEffectInstance> PassiveEffects => _passiveEffects;
    public IReadOnlyList<TriggeredEffectInstance> TriggeredEffects => _triggeredEffects;

    public DamageReceived Damage(int amount)
    {
        if (IsDead)
        {
            return new DamageReceived(0, 0);
        }

        if (amount > Shield)
        {
            int shieldDamage = Shield;

            SetShield(0);

            int healthDamage = amount - shieldDamage;

            if (healthDamage > Health)
            {
                healthDamage = Health;
            }

            SetHealth(Health - healthDamage);

            return new DamageReceived(healthDamage, shieldDamage);
        }
        SetShield(Shield - amount);
        return new DamageReceived(0, amount);
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
