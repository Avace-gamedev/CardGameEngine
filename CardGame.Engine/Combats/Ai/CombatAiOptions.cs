namespace CardGame.Engine.Combats.Ai;

public class CombatAiOptions
{
    public CombatAiOptions(int randomSeed)
    {
        RandomSeed = randomSeed;
    }

    public int RandomSeed { get; }
}
