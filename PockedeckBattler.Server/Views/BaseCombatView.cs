using CardGame.Engine.Combats;

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
}
