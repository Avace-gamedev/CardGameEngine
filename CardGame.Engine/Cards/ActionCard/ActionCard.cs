using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Active;
using CardGame.Engine.Effects.Enchantments;
using CardGame.Engine.Effects.Enchantments.State;
using CardGame.Engine.Effects.Enchantments.Triggered;

namespace CardGame.Engine.Cards.ActionCard;

public class ActionCard : Card
{
    public ActionCard(
        string name,
        string? description,
        int apCost,
        ActionCardTarget target,
        ActiveEffect mainEffect,
        params ActiveEffect[] additionalEffects
    ) : base(name, description)
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
        CharacterCombatState[] characterCombatStates = targets as CharacterCombatState[] ?? targets.ToArray();

        MainEffect.Resolve(source, characterCombatStates);

        foreach (ActiveEffect effect in AdditionalEffects)
        {
            effect.Resolve(source, characterCombatStates);
        }
    }

    public static ActionCard Damage(string name, int apCost, ActionCardTarget target, int damage, Element element, params ActiveEffect[] additionalEffects)
    {
        return Damage(name, null, apCost, target, damage, element, additionalEffects);
    }

    public static ActionCard Damage(
        string name,
        string? description,
        int apCost,
        ActionCardTarget target,
        int damage,
        Element element,
        params ActiveEffect[] additionalEffects
    )
    {
        return new ActionCard(name, description, apCost, target, new DamageEffect(damage, element), additionalEffects);
    }

    public static ActionCard Heal(string name, string? description, int apCost, ActionCardTarget target, int heal)
    {
        return new ActionCard(name, description, apCost, target, new HealEffect(heal));
    }

    public static ActionCard Shield(string name, string? description, int apCost, ActionCardTarget target, int shield)
    {
        return new ActionCard(name, description, apCost, target, new ShieldEffect(shield));
    }

    public static ActionCard AddPassive(string name, string? description, int apCost, ActionCardTarget target, params PassiveEffect[] effects)
    {
        return new ActionCard(name, description, apCost, target, new AddEnchantmentEffect(new Enchantment(name, effects)));
    }

    public static ActionCard AddTriggered(string name, string? description, int apCost, ActionCardTarget target, params TriggeredEffect[] effects)
    {
        return new ActionCard(name, description, apCost, target, new AddEnchantmentEffect(new Enchantment(name, effects)));
    }

    public static ActionCard AddEnchantment(string name, string? description, int apCost, ActionCardTarget target, AddEnchantmentEffect enchantmentEffect)
    {
        return new ActionCard(name, description, apCost, target, enchantmentEffect);
    }
}
