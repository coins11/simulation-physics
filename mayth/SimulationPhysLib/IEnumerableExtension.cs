using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhysLib
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            foreach (var element in source)
                yield return func(element);
        }

        public static IEnumerable<T> RepeatApply<T>(this IEnumerable<T> source, int repeatCount, Func<T, T> func)
        {
            IEnumerable<T> tmp = source;
            foreach (int i in Enumerable.Range(0, repeatCount))
                tmp = tmp.Select(x => func(x));

            return tmp;
        }
    }
}
