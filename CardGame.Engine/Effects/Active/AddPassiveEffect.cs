﻿using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Passive;

namespace CardGame.Engine.Effects.Active;

public class AddPassiveEffect : ActiveEffect
{
    public AddPassiveEffect(PassiveEffect effect)
    {
        Effect = effect;
    }

    public PassiveEffect Effect { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddEffect(Effect, source);
        }
    }
}
