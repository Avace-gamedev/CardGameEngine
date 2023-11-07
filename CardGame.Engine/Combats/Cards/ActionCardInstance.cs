using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Combats.Modifiers;
using CardGame.Engine.Effects.Enchantments.Passive.Stats;

namespace CardGame.Engine.Combats.Cards;

public class ActionCardInstance
{
    readonly Random _random;

    public ActionCardInstance(ActionCard card, CharacterCombatState character, Random random)
    {
        Card = card;
        Character = character;

        _random = random;
    }

    public ActionCard Card { get; }
    public CharacterCombatState Character { get; }

    CombatState Combat => Character.Combat;

    public ActionCard GetCardWithModifications()
    {
        ChangeCardStatEffect[] cardStatEffects = Character.Enchantments.SelectMany(e => e.Enchantment.Passive).OfType<ChangeCardStatEffect>().ToArray();
        CardStatsModifier modifiers = cardStatEffects.Any()
            ? cardStatEffects.Select(e => e.GetStatsModifier()).Aggregate(CardStatsModifier.Combine)
            : CardStatsModifier.None;

        return modifiers.Apply(Card);
    }

    public void Resolve()
    {
        ActionCard cardWithModifications = GetCardWithModifications();

        CombatSideState side = Combat.GetSide(Character.Side);
        side.ConsumeAp(cardWithModifications.ApCost);

        IEnumerable<CharacterCombatState> targets = Combat.GetTargets(Character, cardWithModifications.Target);
        cardWithModifications.Resolve(Character, targets, _random);
    }
}
