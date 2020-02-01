using System;
using Xunit;

namespace Numbers.Tests
{
    using static Functions;

    public class FunctionsShould
    {
        [Theory]
        [InlineData(2, 1, 1)]
        [InlineData(24, 18, 6)]
        [InlineData(18, 24, 6)]
        [InlineData(-18, 24, 6)]
        [InlineData(-2, -1, 1)]
        public void GetGCDOfTwoNumbers(int i, int j, int gcd)
            => Assert.Equal(gcd, GCD(i, j));

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(18, 24, 72)]
        [InlineData(18, -24, 72)]
        [InlineData(2, 2, 2)]
        public void GetLCMOfTwoNumbers(int i, int j, int lcm)
            => Assert.Equal(lcm, LCM(i, j));
    }
}