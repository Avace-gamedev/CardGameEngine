using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Combats.Modifiers;
using CardGame.Engine.Combats.State;
using CardGame.Engine.Effects.Passive.Stats;

namespace CardGame.Engine.Combats;

public class ActionCardInstance
{
    public ActionCardInstance(ActionCard card, CharacterCombatState character)
    {
        Card = card;
        Character = character;
    }

    public ActionCard Card { get; }
    public CharacterCombatState Character { get; }

    public ActionCard GetCardWithModifications()
    {
        CardStatEffect[] cardStatEffects = Character.PassiveEffects.Select(e => e.Effect).OfType<CardStatEffect>().ToArray();
        CardStatsModifier modifiers = cardStatEffects.Any()
            ? cardStatEffects.Select(e => e.GetStatsModifier()).Aggregate(CardStatsModifier.Combine)
            : CardStatsModifier.None;

        return modifiers.Apply(Card);
    }

    public void Resolve(CombatState combat)
    {
        ActionCard cardWithModifications = GetCardWithModifications();

        CombatSideState side = combat.GetSide(Character.Side);
        side.ConsumeAp(cardWithModifications.ApCost);

        IEnumerable<CharacterCombatState> targets = combat.GetTargets(Character, cardWithModifications.Target);
        cardWithModifications.Resolve(Character, targets);
    }
}
