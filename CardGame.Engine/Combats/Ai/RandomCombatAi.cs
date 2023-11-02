using CardGame.Engine.Extensions;

namespace CardGame.Engine.Combats.Ai;

public class RandomCombatAi : CombatAi
{
    public RandomCombatAi(CombatInstance combat, CombatSide side) : base(combat, side)
    {
    }

    protected override void PlayCards()
    {
        while (PlayRandomCard())
        {
            // nothing to do
        }
    }

    bool PlayRandomCard()
    {
        int[] playableCards = PlayableCards().ToArray();
        if (playableCards.Length == 0)
        {
            return false;
        }

        int card = Random.Shared.Sample(playableCards);
        PlayCard(card);

        return true;
    }
}
