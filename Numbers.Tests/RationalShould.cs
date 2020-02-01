using System;
using System.Collections.Generic;
using Xunit;

namespace Numbers.Tests
{
    public class RationalShould
    {
        private static readonly Rational Zero = new Rational(0);
        private static readonly Rational One = new Rational(1, 1);
        private static readonly Rational MinusOne = new Rational(-1);
        private static readonly Rational Two = new Rational(2, 1);
        private static readonly Rational Half = new Rational(1, 2);

        [Theory]
        [InlineData(1, 2, "1/2")]
        [InlineData(5, 1, "5")]
        [InlineData(0, 1, "0")]
        [InlineData(0, -1, "0")]
        [InlineData(0, 100, "0")]
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

        [Theory]
        [MemberData(nameof(GetAdditionTestCases))]
        public void AddTwoNumbers(Rational i, Rational j, Rational result)
            => Assert.Equal(result, i + j);

        public static IEnumerable<object[]> GetAdditionTestCases()
        {
            yield return new object[] {One, One, Two};
            yield return new object[] {Half, Half, One};
            yield return new object[] {MinusOne, One, Zero};
            yield return new object[] {new Rational(5, 2), new Rational(-7, 2), MinusOne};
        }

        [Theory]
        [MemberData(nameof(GetSubtractionTestCases))]
        public void SubtractOneNumberFromAnother(Rational i, Rational j, Rational result)
            => Assert.Equal(result, i - j);

        public static IEnumerable<object[]> GetSubtractionTestCases()
        {
            yield return new object[] {Two, One, One};
            yield return new object[] {One, One, Zero};
            yield return new object[] {Half, Half, Zero};
            yield return new object[] {One, Half, Half};
            yield return new object[] {One, MinusOne, Two};
            yield return new object[] {MinusOne, One, new Rational(-2)};
            yield return new object[] {MinusOne, MinusOne, Zero};
        }
    }
}
