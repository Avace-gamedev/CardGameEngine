using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class CombatSideView : BaseCombatSideView
{
    public CombatSideView(CombatSide side, int handSize, CharacterCombatView frontCharacter, CharacterCombatView? backCharacter = null) : base(
        side,
        frontCharacter,
        backCharacter
    )
    {
        HandSize = handSize;
    }

    int HandSize { get; }
}

public static class CombatSideViewMappingExtensions
{
    public static CombatSideView View(this CombatInstance.CombatSideInstance side)
    {
        return new CombatSideView(side.Side, side.Hand.Count, side.Front.View(), side.Back?.View());
    }
}
