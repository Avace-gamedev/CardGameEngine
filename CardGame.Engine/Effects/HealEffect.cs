using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects;

public class HealEffect : Effect
{
    public HealEffect(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }

    internal override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.Heal(Amount);
        }
    }
}
