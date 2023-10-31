using CardGameEngine.Combats;

namespace CardGameEngine.Effects.Active;

public abstract class ActiveEffect
{
    public abstract void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets);
}
