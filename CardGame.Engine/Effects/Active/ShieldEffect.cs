using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects.Active;

public class ShieldEffect : ActiveEffect
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
