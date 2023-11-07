using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects;

public class RandomEffect : Effect
{
    public RandomEffect(params Entry[] entries)
    {
        if (entries.Length == 0)
        {
            throw new Exception("Expected at least one effect");
        }

        Entries = entries;
    }

    public Entry[] Entries { get; }

    internal override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random)
    {
        double randomDouble = random.NextDouble();
        Effect? randomlySelectedEffect = Entries.Aggregate<Entry, (double Sum, Effect? Effect)>(
                (0, null),
                (acc, entry) =>
                {
                    double newSum = acc.Sum + entry.Probability;
                    return (newSum, acc.Sum >= randomDouble
                        ? acc.Effect
                        : newSum >= randomDouble
                            ? entry.Effect
                            : null);
                }
            )
            .Effect;

        randomlySelectedEffect?.Resolve(source, targets, random);
    }

    public static RandomEffect Uniform(params Effect?[] effects)
    {
        if (effects.Length == 0)
        {
            throw new Exception("Expected at least one effect");
        }

        double probability = 1.0 / effects.Length;
        return new RandomEffect(effects.Where(e => e != null).Select(e => new Entry(e, probability)).ToArray());
    }

    public class Entry
    {
        public Entry(Effect effect, double probability)
        {
            Effect = effect;
            Probability = probability;
        }

        public Effect Effect { get; }
        public double Probability { get; }
    }
}
