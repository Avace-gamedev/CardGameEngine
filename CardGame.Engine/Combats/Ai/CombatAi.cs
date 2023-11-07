using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Ai;

public abstract class CombatAi
{
    protected CombatAi(CombatInstance combat, CombatSide side)
    {
        Side = side;
        Combat = combat;
    }

    protected CombatInstance Combat { get; }
    protected CombatSide Side { get; }

    public void PlayTurn()
    {
        if (Combat.State.Side != Side)
        {
            return;
        }

        PlayCards();

        if (Combat.State.Ongoing)
        {
            Combat.EndTurn(Side);
        }
    }

    protected abstract void PlayCards();

    protected IEnumerable<int> PlayableCards()
    {
        if (!Combat.State.Ongoing)
        {
            return Enumerable.Empty<int>();
        }

        CombatSideState side = Combat.State.GetSide(Side);
        return side.Hand.Select((card, index) => new { Card = card, Index = index })
            .Where(x => x.Card.GetCardWithModifications().ApCost <= side.Ap)
            .Select(x => x.Index);
    }

    protected void PlayCard(int index)
    {
        Combat.PlayCard(Side, index);
    }
}
