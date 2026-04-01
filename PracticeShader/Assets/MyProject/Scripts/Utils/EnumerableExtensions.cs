using System.Collections.Generic;

namespace Utils
{
    /// <summary>
    /// リストをシャッフルする破壊的な拡張メソッド
    /// </summary>
    public static class EnumerableExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            var rand = new System.Random();
            for (int i=0; i<list.Count; i++)
            {
                int j = rand.Next(i, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}