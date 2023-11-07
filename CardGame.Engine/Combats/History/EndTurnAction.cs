using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.History;

public class EndTurnAction : CombatAction
{
    public EndTurnAction(CombatSide side)
    {
        Side = side;
    }

    public CombatSide Side { get; }
}
