﻿using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Ai;

public static class CombatAiFactory
{
    public static CombatAi CreateInstance(CombatInstance combat, CombatSide side, CombatAiOptions options)
    {
        return new RandomCombatAi(combat, side, new Random(options.RandomSeed));
    }
}
