using CardGame.Engine.Combats.Ai;

namespace CardGame.Engine.Combats;

public class CombatOptions
{
    public CombatSide StartingSide { get; init; } = CombatSide.Left;
    public int HandSizeWithBothCharacters { get; set; } = 6;
    public int HandSizeWithOneCharacter { get; set; } = 3;
    public int StartingAp { get; init; } = 4;
    public int MaxAp { get; init; } = 10;

    public CombatAiOptions? LeftSideAi { get; init; }
    public CombatAiOptions? RightSideAi { get; init; }
}
