using CardGame.Engine.Effects.Active;

namespace CardGame.Engine.Combats.Modifiers;

public static class ActiveEffectModifierExtensions
{
    public static DamageEffect ChangeDamageAmount(this DamageEffect damageEffect, int newDamage)
    {
        return new DamageEffect(Math.Max(0, newDamage), damageEffect.Element) { LifeStealRatio = damageEffect.LifeStealRatio };
    }
}
