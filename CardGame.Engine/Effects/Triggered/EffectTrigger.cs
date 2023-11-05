using CardGame.Engine.Combats;
using CardGame.Engine.Combats.State;
using CardGame.Engine.Effects.Triggered.Instance;

namespace CardGame.Engine.Effects.Triggered;

public abstract class EffectTrigger
{
    public abstract TriggerState CreateNewState(CombatState combat, CharacterCombatState source, CharacterCombatState target);
}
