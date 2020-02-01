using System;
using System.Collections.Generic;
using Xunit;

namespace Numbers.Tests
{
    public class RationalShould
    {
        [Theory]
        [InlineData(1, 2, "1/2")]
        [InlineData(5, 1, "5")]
        [InlineData(0, 1, "0")]
        [InlineData(0, -1, "0")]
        [InlineData(0, 100, "0")]
        [InlineData(2, 4, "1/2")]
        [InlineData(2, 4, "1/2")]
        [InlineData(2, 4, "1/2")]
        [InlineData(-1, -1, "1")]
        [InlineData(-1, -2, "1/2")]
        [InlineData(1, -2, "-1/2")]
        [InlineData(-1, 2, "-1/2")]
        public void SimplifyRationals(int i, int j, string result)
            => Assert.Equal(result, new Rational(i, j).ToString());

        [Fact]
        public void ThrowDivideByZeroException()
            => Assert.Throws<DivideByZeroException>(() => new Rational(1, 0));

        [Theory]
        [MemberData(nameof(GetEqualityTestCases))]
        public void HaveEqualityOperatorReturnCorrectly(Rational i, Rational j, bool equal)
            => Assert.Equal(equal, i == j);

        public static IEnumerable<object[]> GetEqualityTestCases()
        {
            yield return new object[] {new Rational(1, 1), new Rational(1, 1), true};
            yield return new object[] {new Rational(2, 1), new Rational(1, 1), false};
            yield return new object[] {new Rational(1, 2), new Rational(8, 16), true};
            yield return new object[] {new Rational(1, 2), new Rational(1, 1), false};
            yield return new object[] {new Rational(0, 2), new Rational(0, 1), true};
            yield return new object[] {new Rational(-1, -2), new Rational(1, 2), true};
            yield return new object[] {new Rational(-1, -2), new Rational(-1, 2), false};
            yield return new object[] {new Rational(-1, 2), new Rational(1, -2), true};
        }

        [Theory]
        [MemberData(nameof(GetInequalityTestCases))]
        public void HaveInequalityOperatorReturnCorrectly(Rational i, Rational j, bool equal)
            => Assert.Equal(equal, i != j);

        public static IEnumerable<object[]> GetInequalityTestCases()
        {
            foreach (var arr in GetEqualityTestCases())
            {
                arr[2] = !((bool) arr[2]);
                yield return arr;
            }
        }
    }
}
