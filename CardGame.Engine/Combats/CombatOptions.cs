namespace CardGame.Engine.Combats;

public class CombatOptions
{
    public CombatSide StartingSide { get; init; } = CombatSide.Left;
    public int HandSize { get; set; } = 6;
    public int StartingAp { get; init; } = 4;
    public int MaxAp { get; init; } = 10;
}
