﻿using CardGame.Engine.Combats;

namespace PockedeckBattler.Views;

public class PlayerSideView : BaseCombatSideView
{
    public PlayerSideView(
        CombatSide side,
        CardInstanceWithModifiersView[] hand,
        CharacterCombatView frontCharacter,
        CharacterCombatView? backCharacter = null
    ) : base(side, frontCharacter, backCharacter)
    {
        Hand = hand;
    }

    public CardInstanceWithModifiersView[] Hand { get; }
}

public static class PlayerSideViewMappingExtensions
{
    public static PlayerSideView PlayerView(this CombatInstance.CombatSideInstance side)
    {
        return new PlayerSideView(side.Side, side.Hand.Select(c => c.ViewWithModifiers()).ToArray(), side.Front.View(), side.Back?.View());
    }
}
