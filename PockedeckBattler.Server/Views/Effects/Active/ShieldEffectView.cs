using CardGame.Engine.Effects;

namespace PockedeckBattler.Server.Views.Effects.Active;

public class ShieldEffectView : EffectView
{
    public ShieldEffectView(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }
}

public static class ShieldEffectViewMappingExtensions
{
    public static ShieldEffectView View(this ShieldEffect effect)
    {
        return new ShieldEffectView(effect.Amount);
    }
}
