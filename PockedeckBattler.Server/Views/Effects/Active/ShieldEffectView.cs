using CardGame.Engine.Effects.Active;

namespace PockedeckBattler.Server.Views.Effects.Active;

public class ShieldEffectView : ActiveEffectView
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
