using CardGame.Engine.Effects.Active;

namespace PockedeckBattler.Server.Controllers.Views.Effects.Active;

public class RandomEffectView : ActiveEffectView
{
    public RandomEffectView(params RandomEffectEntryView[] entries)
    {
        Entries = entries;
    }

    public RandomEffectEntryView[] Entries { get; }
}

public class RandomEffectEntryView
{
    public RandomEffectEntryView(ActiveEffectView effect, double probability)
    {
        Effect = effect;
        Probability = probability;
    }

    public ActiveEffectView Effect { get; }
    public double Probability { get; }
}

public static class RandomEffectViewMappingExtensions
{
    public static RandomEffectView View(this RandomEffect effect)
    {
        return new RandomEffectView(effect.Entries.Select(e => e.View()).ToArray());
    }

    public static RandomEffectEntryView View(this RandomEffect.Entry entry)
    {
        return new RandomEffectEntryView(entry.Effect.View(), entry.Probability);
    }
}
