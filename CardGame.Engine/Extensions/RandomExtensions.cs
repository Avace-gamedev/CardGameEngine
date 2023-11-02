﻿namespace CardGame.Engine.Extensions;

public static class RandomExtensions
{
    public static void Shuffle<T>(this Random rng, IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public static T Sample<T>(this Random rng, IReadOnlyList<T> list)
    {
        int randomIndex = rng.Next(0, list.Count);
        return list[randomIndex];
    }
}
