using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Combats.Modifiers;

namespace CardGame.Engine.Effects.Active;

public class DamageEffect : ActiveEffect
{
    public DamageEffect(int amount, Element element)
    {
        Amount = amount;
        Element = element;
    }

    public int Amount { get; }
    public Element Element { get; }
    public float LifeStealRatio { get; init; } = 0;

    internal override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random)
    {
        AttackDamage attackDamage = new(Amount);

        CharacterStatsModifier sourceModifiers = source.GetStatsModifier();
        AttackDamage actualAttackDamage = sourceModifiers.ModifyAttackToDeal(attackDamage);

        foreach (CharacterCombatState target in targets)
        {
            DamageReceived damage = target.Damage(actualAttackDamage);

            int stolenHp = (int)(damage.Health * LifeStealRatio);
            if (stolenHp > 0)
            {
                source.Heal(stolenHp);
            }
        }
    }
}
