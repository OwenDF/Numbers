using System;
using System.Collections.Generic;

namespace Numbers
{
    public static class EnumerableExtensions
    {
        public static Rational Sum(this IEnumerable<Rational> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new Rational(0);
            foreach(var i in source) result += i;
            return result;
        }

        // The following are internal as they assume both sources are the same length,
        // and should only be called if that is the case.
        internal static IEnumerable<TR> Select<TS, TR>(this (IEnumerable<TS>, IEnumerable<TS>) sources, Func<TS, TS, TR> selector)
        {
            var (first, second) = sources;
            var secondEnumerator = second.GetEnumerator();

            foreach(var i in first)
            {
                secondEnumerator.MoveNext();
                var j = secondEnumerator.Current;
                yield return selector(i, j);
            }
        }

        internal static bool All<T>(this (IEnumerable<T>, IEnumerable<T>) sources, Func<T, T, bool> predicate)
        {
            var (first, second) = sources;
            var secondEnumerator = second.GetEnumerator();

            foreach(var i in first)
            {
                secondEnumerator.MoveNext();
                var j = secondEnumerator.Current;
                if (!predicate(i, j)) return false;
            }

            return true;
        }
    }
}