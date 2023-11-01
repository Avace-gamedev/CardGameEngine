using CardGame.Engine.Combats;

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

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            DamageReceived damage = target.Damage(Amount);

            int stolenHp = (int)(damage.Health * LifeStealRatio);
            if (stolenHp > 0)
            {
                source.Heal(stolenHp);
            }
        }
    }
}
