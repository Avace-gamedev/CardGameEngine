namespace CardGameEngine.Extensions;

public static class RandomExtensions
{
    public static void Shuffle<T>(this Random rng, IList<T> array)
    {
        int n = array.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}
