using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.History;

public class InitialCombatState
{
    public InitialCombatState(int randomSeed, IReadOnlyList<Character> leftCharacters, IReadOnlyList<Character> rightCharacters, CombatOptions options)
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
