using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects;

public abstract class Effect
{
    internal abstract void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random);
}
