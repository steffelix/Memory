using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;


namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        private static Random rnd = new Random();
        public static List<TSource> Shuffle<TSource>(this List<TSource> source)
        {
            var hold = new List<TSource>();

            var count = source.Count;

            for (int i = 0; i < count; i++)
            {
                hold.Add(source[i]);
            }

            var result = new List<TSource>();

            for (int i = 0; i < count; i++)
            {
                var index = rnd.Next(0, hold.Count);
                result.Add(hold[index]);
                hold.RemoveAt(index);
            }

            return result;
        }
    }
}
