using CardGame.Engine.Cards.ActionCard;

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

    public int ApCost => Card.ApCost - Character.StatsModifier.ApCostAdditiveModifier;

    public void Resolve()
    {
        IEnumerable<CharacterCombatState> targets = Card.Target.GetTargets(Character);
        Card.Resolve(Character, targets);
    }
}
