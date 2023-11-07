using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Extensions;

namespace CardGame.Engine.Combats.Ai;

public class RandomCombatAi : CombatAi
{
    readonly Random _random;

    public RandomCombatAi(CombatInstance combat, CombatSide side, Random random) : base(combat, side)
    {
        _random = random;
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

        int card = _random.Sample(playableCards);
        PlayCard(card);

        return true;
    }
}
