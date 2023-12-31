﻿using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats.Abstractions;
using PockedeckBattler.Server.Views.Combats.Log;

namespace PockedeckBattler.Server.Views;

public class BaseCombatView
{
    public BaseCombatView(int turn, int maxAp, CombatSide currentSide, CombatSideTurnPhase currentPhase)
    {
        Turn = turn;
        MaxAp = maxAp;
        CurrentSide = currentSide;
        CurrentPhase = currentPhase;
    }

    public bool Ongoing { get; init; }
    public bool Over { get; init; }
    public int Turn { get; }
    public int MaxAp { get; }
    public CombatSide CurrentSide { get; }
    public CombatSideTurnPhase CurrentPhase { get; }
    public CombatSide Winner { get; init; }

    [Required]
    public string LeftPlayerName { get; init; } = "";

    [Required]
    public string RightPlayerName { get; init; } = "";

    [Required]
    public CombatLogView Log { get; init; }
}
