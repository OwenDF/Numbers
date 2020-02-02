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

        [Theory]
        [InlineData(0, 342, 0)]
        [InlineData(1, 3424, 1)]
        [InlineData(123, 0, 1)]
        [InlineData(-1, 2, 1)]
        [InlineData(-1, 3, -1)]
        [InlineData(2, 4, 16)]
        [InlineData(-2, 3, -8)]
        [InlineData(0, 0, 1)]
        public void RaiseIntsToPowers(int x, int e, int result)
            => Assert.Equal(result, x.ToPower(e));

        [Fact]
        public void ThrowArgExceptionForNegativePowers()
        {
            var e = Assert.Throws<ArgumentException>(() => 1.ToPower(-1));
            Assert.Equal("Cannot raise to a negative power using this method (Parameter 'exponent')", e.Message);
        }
    }
}