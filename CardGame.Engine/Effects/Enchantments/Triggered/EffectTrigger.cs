using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace CardGame.Engine.Effects.Enchantments.Triggered;

public abstract class EffectTrigger
{
    public abstract TriggerState CreateNewState(CombatState combat, CharacterCombatState source, CharacterCombatState target);
}
