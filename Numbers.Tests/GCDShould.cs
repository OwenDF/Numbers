using System;
using Xunit;

namespace Numbers.Tests
{
    using static Functions;

    public class GCDShould
    {
        [Theory]
        [InlineData(2, 1, 1)]
        [InlineData(24, 18, 6)]
        [InlineData(18, 24, 6)]
        [InlineData(-18, 24, 6)]
        [InlineData(-2, -1, 1)]
        public void GetGCDOfTwoNumbers(int first, int second, int gcd)
            => Assert.Equal(gcd, GCD(first, second));
    }
}