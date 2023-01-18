namespace Numbers.Matrices.Tests
{
    using R = Rational;
    using RM = RationalMatrix;

    internal static class TestRationalMatrices
    {
        public static RM TwoByTwoIdentity => new RM(new[] {new R[] {1, 0}, new R[] {0, 1}});
        public static RM TwoByTwo => new RM(new[] {new R[] {1, 2}, new R[] {3, 4}});
        public static RM TwoByThree => new RM(new[] {new R[] {1, 2, 3}, new R[] {4, 5, 6}});
        public static RM ThreeByTwo => new RM(new[] {new R[] {1, 2}, new R[] {3, 4}, new R[] {5, 6}});
        public static RM ThreeByThreeIdentity => new RM(new[] {new R[] {1, 0, 0}, new R[] {0, 1, 0}, new R[] {0, 0, 1}});
        public static RM ThreeByThree => new RM(new[] {new R[] {10, 4, -1}, new R[] {-7, -4, 0}, new R[] {1, 16, 10}});
        public static RM ThreeByThreeInv => new RM(new[] {new R[] {40, 56, 4}, new R[] {-70, -101, -7}, new R[] {108, 156, 12}}) * new R(1, 12);

        // Matrices from my assignment:
        public static readonly R[][] A = {new R[] {4, -1}, new R[] {2, 3}};
        public static readonly R[][] B = {new R[] {0, 2}, new R[] {1, 0}};
        public static readonly R[][] C = {new R[] {1, -3}, new R[] {-1, 2}, new R[] {4, 1}};
        public static readonly R[][] D = {new R[] {1, 4, 2}, new R[] {-3, 0, 1}};
        public static readonly R[][] AB = {new R[] {-1, 8}, new R[] {3, 4}};
        public static readonly R[][] CD = {new R[] {10, 4, -1}, new R[] {-7, -4, 0}, new R[] {1, 16, 9}};
        public static readonly R[][] AD = {new R[] {7, 16, 7}, new R[] {-7, 8, 7}};

        public static readonly R[][] Q = {new R[] {-5, 1}, new R[] {-2, 3}};
        public static readonly R[][] QInv = {new[] {new R(-3, 13), new R(1, 13)}, new[] {new R(-2, 13), new R(5, 13)}};

        public static readonly R[][] r = {new R[] {1, 0, 2}, new R[] {0, -1, -1}, new R[] {2, -1, 2}};
        public static readonly R[][] RInv = {new R[] {-3, -2, 2}, new R[] {-2, -2, 1}, new R[] {2, 1, -1}};

    }
}