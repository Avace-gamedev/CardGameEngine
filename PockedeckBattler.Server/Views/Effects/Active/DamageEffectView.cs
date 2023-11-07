using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Effects;

namespace PockedeckBattler.Server.Views.Effects.Active;

public class DamageEffectView : EffectView
{
    public DamageEffectView(int amount, Element element, float lifeStealRatio)
    {
        Amount = amount;
        Element = element;
        LifeStealRatio = lifeStealRatio;
    }

    public int Amount { get; }
    public Element Element { get; }
    public float LifeStealRatio { get; }
}

public static class DamageEffectViewMappingExtensions
{
    public static DamageEffectView View(this DamageEffect effect)
    {
        return new DamageEffectView(effect.Amount, effect.Element, effect.LifeStealRatio);
    }
}
