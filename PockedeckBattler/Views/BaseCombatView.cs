using CardGame.Engine.Combats;

namespace PockedeckBattler.Views;

public class BaseCombatView
{
    public BaseCombatView(int turn, CombatSide currentSide, CombatSideTurnPhase currentPhase)
    {
        Turn = turn;
        CurrentSide = currentSide;
        CurrentPhase = currentPhase;
    }

    public bool Ongoing { get; init; }
    public bool Over { get; init; }
    public int Turn { get; }
    public CombatSide CurrentSide { get; }
    public CombatSideTurnPhase CurrentPhase { get; }
}
