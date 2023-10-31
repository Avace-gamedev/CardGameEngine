using CardGameEngine.Combats;

namespace CardGameEngine.Effects.Active;

public class DamageEffect : ActiveEffect
{
    public DamageEffect(int amount, Element element)
    {
        Amount = amount;
        Element = element;
    }

    public int Amount { get; }
    public Element Element { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.Damage(Amount);
        }
    }
}
