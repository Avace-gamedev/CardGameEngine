using CardGame.Engine.Effects.Active;
using PockedeckBattler.Server.Controllers.Views.Effects.Triggered;

namespace PockedeckBattler.Server.Controllers.Views.Effects.Active;

public class AddTriggeredEffectView : ActiveEffectView
{
    public AddTriggeredEffectView(TriggeredEffectView passiveEffect)
    {
        TriggeredEffect = passiveEffect;

    }

    public TriggeredEffectView TriggeredEffect { get; }
}

public static class AddTriggeredEffectViewMappingExtensions
{
    public static AddTriggeredEffectView View(this AddTriggeredEffect effect)
    {
        return new AddTriggeredEffectView(effect.Effect.View());
    }
}
