using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects;
using CardGame.Engine.Effects.Enchantments;
using CardGame.Engine.Effects.Enchantments.Passive;
using CardGame.Engine.Effects.Enchantments.Triggered;

namespace CardGame.Engine.Cards.ActionCard;

public class ActionCard : Card
{
    public ActionCard(
        string name,
        string? description,
        int apCost,
        ActionCardTarget target,
        Effect mainEffect,
        IReadOnlyList<Effect>? additionalEffects = null
    ) : base(name, description)
    {
        ApCost = apCost;
        MainEffect = mainEffect;
        Target = target;
        AdditionalEffects = additionalEffects?.ToList() ?? new List<Effect>();
    }

    public int ApCost { get; }
    public ActionCardTarget Target { get; }
    public Effect MainEffect { get; }
    public IReadOnlyList<Effect> AdditionalEffects { get; }

    internal void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random)
    {
        CharacterCombatState[] characterCombatStates = targets as CharacterCombatState[] ?? targets.ToArray();

        MainEffect.Resolve(source, characterCombatStates, random);

        foreach (Effect effect in AdditionalEffects)
        {
            effect.Resolve(source, characterCombatStates, random);
        }
    }

    public static ActionCard Damage(string name, int apCost, ActionCardTarget target, int damage, Element element, params Effect[] additionalEffects)
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
        params Effect[] additionalEffects
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
        return new ActionCard(name, description, apCost, target, new AddEnchantmentEffect(Enchantment.CreateInstance(name, effects)));
    }

    public static ActionCard AddTriggered(string name, string? description, int apCost, ActionCardTarget target, params TriggeredEffect[] effects)
    {
        return new ActionCard(name, description, apCost, target, new AddEnchantmentEffect(Enchantment.CreateInstance(name, effects)));
    }

    public static ActionCard AddEnchantment(string name, string? description, int apCost, ActionCardTarget target, AddEnchantmentEffect enchantmentEffect)
    {
        return new ActionCard(name, description, apCost, target, enchantmentEffect);
    }
}
