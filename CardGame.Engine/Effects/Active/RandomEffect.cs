using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects.Active;

public class RandomEffect : ActiveEffect
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
        ActiveEffect? randomlySelectedEffect = Entries.Aggregate<Entry, (double Sum, ActiveEffect? Effect)>(
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

    public static RandomEffect Uniform(params ActiveEffect?[] effects)
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
        public Entry(ActiveEffect effect, double probability)
        {
            Effect = effect;
            Probability = probability;
        }

        public ActiveEffect Effect { get; }
        public double Probability { get; }
    }
}
