using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Active;

public class HealEffect : ActiveEffect
{
    public HealEffect(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.Heal(Amount);
        }
    }
}
