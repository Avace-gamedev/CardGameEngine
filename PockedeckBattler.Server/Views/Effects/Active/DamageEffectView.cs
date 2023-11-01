using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Active;

namespace PockedeckBattler.Server.Views.Effects.Active;

public class DamageEffectView : ActiveEffectView
{
    public DamageEffectView(int amount, Element element)
    {
        Amount = amount;
        Element = element;
    }

    public int Amount { get; }
    public Element Element { get; }
}

public static class DamageEffectViewMappingExtensions
{
    public static DamageEffectView View(this DamageEffect effect)
    {
        return new DamageEffectView(effect.Amount, effect.Element);
    }
}
