using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects;

public class ShieldEffect : Effect
{
    public ShieldEffect(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }

    internal override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddShield(Amount);
        }
    }
}
