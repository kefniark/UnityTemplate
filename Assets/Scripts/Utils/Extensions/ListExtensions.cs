using System.Collections.Generic;

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        int count = ts.Count;
        int last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            int r = UnityEngine.Random.Range(i, count);
            T tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
