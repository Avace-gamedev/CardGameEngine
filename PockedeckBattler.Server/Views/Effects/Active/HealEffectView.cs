using CardGame.Engine.Effects.Active;

namespace PockedeckBattler.Server.Views.Effects.Active;

public class HealEffectView : ActiveEffectView
{
    public HealEffectView(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }
}

public static class HealEffectViewMappingExtensions
{
    public static HealEffectView View(this HealEffect effect)
    {
        return new HealEffectView(effect.Amount);
    }
}
