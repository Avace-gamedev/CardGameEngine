using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Combats.State;

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

    public void Resolve(CombatState combat)
    {
        IEnumerable<CharacterCombatState> targets = combat.GetTargets(Character, Card.Target);
        Card.Resolve(Character, targets);
    }
}
