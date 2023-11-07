using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.History;

public class PlayCardAction : CombatAction
{
    public PlayCardAction(CombatSide side, int cardIndex)
    {
        Side = side;
        CardIndex = cardIndex;
    }

    public CombatSide Side { get; }
    public int CardIndex { get; }
}
