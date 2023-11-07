using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects.Active;

public abstract class ActiveEffect
{
    internal abstract void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random);
}
