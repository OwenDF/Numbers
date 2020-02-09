using Numbers;

namespace Numbers.Tests
{
    using R = Rational;
    using RM = RationalMatrix;

    internal static class TestRationalMatrices
    {
        public static RM TwoByTwo => new RM(new R[][] {new R[] {1, 2}, new R[] {3, 4}});
        public static RM TwoByThree => new RM(new R[][] {new R[] {1, 2, 3}, new R[] {4, 5, 6}});
        public static RM ThreeByTwo => new RM(new R[][] {new R[] {1, 2}, new R[] {3, 4}, new R[] {5, 6}});

        // Matrices from my assignment:
        public static readonly R[][] A = new R[][] {new R[] {4, -1}, new R[] {2, 3}};
        public static readonly R[][] B = new R[][] {new R[] {0, 2}, new R[] {1, 0}};
        public static readonly R[][] C = new R[][] {new R[] {1, -3}, new R[] {-1, 2}, new R[] {4, 1}};
        public static readonly R[][] D = new R[][] {new R[] {1, 4, 2}, new R[] {-3, 0, 1}};
        public static readonly R[][] AB = new R[][] {new R[] {-1, 8}, new R[] {3, 4}};
        public static readonly R[][] CD = new R[][] {new R[] {10, 4, -1}, new R[] {-7, -4, 0}, new R[] {1, 16, 9}};
        public static readonly R[][] AD = new R[][] {new R[] {7, 16, 7}, new R[] {-7, 8, 7}};
    }
}