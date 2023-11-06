using CardGame.Engine.Combats;

namespace CardGame.Engine.Effects.Active;

public abstract class ActiveEffect
{
    internal abstract void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random);
}
