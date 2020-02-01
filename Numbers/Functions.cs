using System;

namespace Numbers
{
    using static Math;

    internal static class Functions
    {
        public static int GCD(int i, int j)
        {
            i = Abs(i);
            j = Abs(j);
            return OrderedGCD(Max(i, j), Min(i, j));
        }

        private static int OrderedGCD(int i, int j)
        {
            int remainder;

            do
            {
                remainder = i % j;
                i = j;
                j = remainder;
            } while (remainder != 0);

            return i;
        }
    }
}