using CardGame.Engine.Combats.Damages;

namespace CardGame.Engine.Combats.Modifiers;

public class CharacterStatsModifier
{
    public static CharacterStatsModifier None => new() { ResistanceAdditiveModifier = 0 };

    public int ResistanceAdditiveModifier { get; init; }

    public AttackDamage ModifyAttackToDeal(AttackDamage attackDamage)
    {
        return attackDamage;
    }

    public AttackDamage ModifyReceivedAttack(AttackDamage attackDamage)
    {
        return new AttackDamage(attackDamage.Amount + ResistanceAdditiveModifier);
    }

    public static CharacterStatsModifier Combine(CharacterStatsModifier modifier1, CharacterStatsModifier modifier2)
    {
        return new CharacterStatsModifier
        {
            ResistanceAdditiveModifier = modifier1.ResistanceAdditiveModifier + modifier2.ResistanceAdditiveModifier
        };
    }
}
