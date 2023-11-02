﻿using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;

namespace PockedeckBattler.Server.Views;

public class PlayerSideView : CombatSideView
{
    public PlayerSideView(
        string playerName,
        CombatSide side,
        int ap,
        CardInstanceWithModifiersView[] hand,
        CharacterCombatView frontCharacter,
        CharacterCombatView? backCharacter = null
    ) : base(playerName, side, ap, frontCharacter, backCharacter)
    {
        Hand = hand;
        HandSize = hand.Length;
    }

    [Required]
    public CardInstanceWithModifiersView[] Hand { get; }
}

public static class PlayerSideViewMappingExtensions
{
    public static PlayerSideView PlayerView(this CombatInstance.CombatSideInstance side, string playerName)
    {
        return new PlayerSideView(playerName, side.Side, side.Ap, side.Hand.Select(c => c.ViewWithModifiers()).ToArray(), side.Front.View(), side.Back?.View())
        {
            DeckSize = side.Deck.Count
        };
    }
}
