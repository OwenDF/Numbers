using System;

namespace Numbers
{
    using static Math;

    internal static class Functions
    {
        public static int GCD(int i, int j)
        {
            if (i == 0 || j == 0) throw new ArgumentException("Cannot calculate gcd of 0");
            i = Abs(i);
            j = Abs(j);
            return OrderedGCD(Max(i, j), Min(i, j));
        }

        public static int LCM(int i, int j)
            => (i == 0 || j == 0) ? 
                throw new ArgumentException("Cannot calculate lcm of 0") :
                Abs((i / GCD(i, j)) * j);

        public static int ToPower(this int i, int exponent)
        {
            if (exponent < 0)
                throw new ArgumentException("Cannot raise to a negative power using this method", nameof(exponent));

            var result = 1;

            for (var count = 0; count < exponent; count++)
            {
                result *= i;
            }

            return result;
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