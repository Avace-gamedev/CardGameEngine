using CardGame.Engine.Characters;

namespace CardGame.Engine.Combats.History;

public class InitialCombatState
{
    public InitialCombatState(int randomSeed, IEnumerable<Character> leftCharacters, IEnumerable<Character> rightCharacters, CombatOptions options)
    {
        RandomSeed = randomSeed;
        LeftCharacters = leftCharacters.ToArray();
        RightCharacters = rightCharacters.ToArray();
        Options = options;
    }

    public int RandomSeed { get; }
    public IReadOnlyList<Character> LeftCharacters { get; }
    public IReadOnlyList<Character> RightCharacters { get; }
    public CombatOptions Options { get; }
}
