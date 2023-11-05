using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Triggered;

namespace CardGame.Engine.Effects.Active;

public class AddTriggeredEffect : ActiveEffect
{
    public AddTriggeredEffect(TriggeredEffect effect)
    {
        Effect = effect;
    }

    public TriggeredEffect Effect { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddEffect(Effect, source);
        }
    }
}
