using CardGameEngine.Combats;
using CardGameEngine.Effects.Active;

namespace CardGameEngine.Cards.ActionCard;

public class ActionCard : Card
{
    public ActionCard(string name, string description, int apCost, ActionCardTarget target, ActiveEffect mainEffect, params ActiveEffect[] additionalEffects) : base(
        name,
        description
    )
    {
        ApCost = apCost;
        MainEffect = mainEffect;
        Target = target;
        AdditionalEffects = additionalEffects;
    }

    public int ApCost { get; }
    public ActionCardTarget Target { get; }
    public ActiveEffect MainEffect { get; }
    public IReadOnlyList<ActiveEffect> AdditionalEffects { get; }

    public void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        MainEffect.Resolve(source, targets);
    }

    public static ActionCard Damage(
        string name,
        string description,
        int apCost,
        ActionCardTarget target,
        int damage,
        Element element,
        params ActiveEffect[] additionalEffects
    )
    {
        return new ActionCard(name, description, apCost, target, new DamageEffect(damage, element), additionalEffects);
    }

    public static ActionCard Heal(string name, string description, int apCost, ActionCardTarget target, int heal)
    {
        return new ActionCard(name, description, apCost, target, new HealEffect(heal));
    }

    public static ActionCard Shield(string name, string description, int apCost, ActionCardTarget target, int shield)
    {
        return new ActionCard(name, description, apCost, target, new ShieldEffect(shield));
    }

    public static ActionCard AddPassive(string name, string description, int apCost, ActionCardTarget target, AddPassiveEffect passiveEffect)
    {
        return new ActionCard(name, description, apCost, target, passiveEffect);
    }
}
