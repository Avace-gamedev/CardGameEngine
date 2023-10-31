﻿using CardGameEngine.Combats;

namespace CardGameEngine.Effects.Passive;

public class PassiveStatsModifier : PassiveEffect
{
    public PassiveStatsModifier(StatsModifier statsModifier, int duration) : base(duration)
    {
        StatsModifier = statsModifier;
    }

    public StatsModifier StatsModifier { get; }
}