using CardGameEngine.Combats;

namespace CardGameEngine.Effects.Active;

public class ShieldEffect : ActiveEffect
{
    public ShieldEffect(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddShield(Amount);
        }
    }
}
