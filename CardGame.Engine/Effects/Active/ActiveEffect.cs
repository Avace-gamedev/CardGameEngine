using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Active;

public abstract class ActiveEffect
{
    public abstract void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets);
}
