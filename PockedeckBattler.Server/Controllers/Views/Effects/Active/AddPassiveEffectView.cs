using CardGame.Engine.Effects.Active;
using PockedeckBattler.Server.Controllers.Views.Effects.Passive;

namespace PockedeckBattler.Server.Controllers.Views.Effects.Active;

public class AddPassiveEffectView : ActiveEffectView
{
    public AddPassiveEffectView(PassiveEffectView passiveEffect)
    {
        PassiveEffect = passiveEffect;

    }

    public PassiveEffectView PassiveEffect { get; }
}

public static class AddPassiveEffectViewMappingExtensions
{
    public static AddPassiveEffectView View(this AddPassiveEffect effect)
    {
        return new AddPassiveEffectView(effect.Effect.View());
    }
}
