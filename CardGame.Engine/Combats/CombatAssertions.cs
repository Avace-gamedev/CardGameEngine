using CardGame.Engine.Combats.Exceptions;
using CardGame.Engine.Combats.State;

namespace CardGame.Engine.Combats;

public static class CombatAssertions
{
    public static void AssertNotStarted(this CombatState combat)
    {
        if (combat.Ongoing)
        {
            throw new InvalidMoveException("Already started");
        }
    }

    public static void AssertOngoing(this CombatState combat)
    {
        if (!combat.Ongoing)
        {
            AssertNotOver(combat);

            throw new InvalidMoveException("Combat not started yet");
        }
    }

    public static void AssertSideCanPlay(this CombatState combat, CombatSide side)
    {
        if (combat.Side != side || combat.Phase != CombatSideTurnPhase.Play)
        {
            throw new InvalidMoveException("Not your turn");
        }
    }

    public static void AssertSideTurnStarted(this CombatState combat)
    {
        if (combat.Phase == CombatSideTurnPhase.None)
        {
            throw new InvalidMoveException("Side turn not started yet");
        }
    }

    public static void AssertSideTurnNotStartedYet(this CombatState combat)
    {
        if (combat.Phase != CombatSideTurnPhase.None)
        {
            throw new InvalidMoveException("Side turn already started");
        }
    }

    public static void AssertNotOver(this CombatState combat)
    {
        if (combat.Over)
        {
            throw new InvalidMoveException("Combat already over");
        }
    }
}
