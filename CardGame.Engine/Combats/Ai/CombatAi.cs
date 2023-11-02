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
        if (Combat.Side != Side)
        {
            return;
        }

        PlayCards();

        Combat.EndSideTurnAndStartNextOne(Side);
    }

    protected abstract void PlayCards();

    protected IEnumerable<int> PlayableCards()
    {
        CombatInstance.CombatSideInstance side = Combat.GetSide(Side);
        return side.Hand.Select((card, index) => new { Card = card, Index = index }).Where(x => x.Card.ApCost <= side.Ap).Select(x => x.Index);
    }

    protected void PlayCard(int index)
    {
        Combat.PlayCardAt(Side, index);
    }
}
