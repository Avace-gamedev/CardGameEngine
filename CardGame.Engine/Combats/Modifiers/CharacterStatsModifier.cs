using CardGame.Engine.Combats.Damages;

namespace CardGame.Engine.Combats.Modifiers;

public class CharacterStatsModifier
{
    public static CharacterStatsModifier None => new() { AllResistancesAdditiveModifier = 0 };

    public int AllDamagesAdditiveModifier { get; init; }
    public int AllResistancesAdditiveModifier { get; init; }

    public AttackDamage ModifyAttackToDeal(AttackDamage attackDamage)
    {
        return new AttackDamage(attackDamage.Amount + AllDamagesAdditiveModifier);
    }

    public AttackDamage ModifyReceivedAttack(AttackDamage attackDamage)
    {
        return new AttackDamage(attackDamage.Amount - AllResistancesAdditiveModifier);
    }

    public static CharacterStatsModifier Combine(CharacterStatsModifier modifier1, CharacterStatsModifier modifier2)
    {
        return new CharacterStatsModifier
        {
            AllDamagesAdditiveModifier = modifier1.AllDamagesAdditiveModifier + modifier2.AllDamagesAdditiveModifier,
            AllResistancesAdditiveModifier = modifier1.AllResistancesAdditiveModifier + modifier2.AllResistancesAdditiveModifier
        };
    }
}
