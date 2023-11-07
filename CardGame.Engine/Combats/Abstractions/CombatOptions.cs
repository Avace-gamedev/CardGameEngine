namespace CardGame.Engine.Combats.Abstractions;

public class CombatOptions
{
    int? _randomSeed;
    public int RandomSeed {
        get => _randomSeed ?? Random.Shared.Next();
        set => _randomSeed = value;
    }
    public CombatSide StartingSide { get; init; } = CombatSide.Left;
    public int HandSizeWithBothCharacters { get; set; } = 6;
    public int HandSizeWithOneCharacter { get; set; } = 3;
    public int StartingAp { get; init; } = 4;
    public int MaxAp { get; init; } = 10;
}
