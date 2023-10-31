﻿using CardGame.Engine.Combats;

namespace PockedeckBattler.Views;

public abstract class BaseCombatSideView
{
    public BaseCombatSideView(CombatSide side, CharacterCombatView frontCharacter, CharacterCombatView? backCharacter = null)
    {
        Side = side;
        FrontCharacter = frontCharacter;
        BackCharacter = backCharacter;
    }

    public CombatSide Side { get; }
    public int DeckSize { get; init; }

    public CharacterCombatView FrontCharacter { get; }
    public CharacterCombatView? BackCharacter { get; }
}