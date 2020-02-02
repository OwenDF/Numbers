using System;
using System.Collections.Generic;
using Xunit;

namespace Numbers.Tests
{
    public class RationalShould
    {
        private static readonly Rational Zero = 0;
        private static readonly Rational One = 1;
        private static readonly Rational MinusOne = -1;
        private static readonly Rational Two = 2;
        private static readonly Rational Four = 4;
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

        [Fact]
        public void CastToDouble() => Assert.True(0.5 == Half);

        [Fact]
        public void CastFromInt() => Assert.True(1 == One);

        [Theory]
        [MemberData(nameof(GetEqualityTestCases))]
        public void HaveEqualityOperatorReturnCorrectly(Rational i, Rational j, bool equal)
            => Assert.Equal(equal, i == j);

        [Theory]
        [MemberData(nameof(GetEqualityTestCases))]
        public void HaveEqualsMethodReturnCorrectly(Rational i, Rational j, bool equal)
            => Assert.Equal(equal, i.Equals(j));

        [Theory]
        [MemberData(nameof(GetEqualityTestCases))]
        public void HaveGetHashCodeFunctionCorrectly(Rational i, Rational j, bool equal)
            => Assert.Equal(equal, i.GetHashCode() == j.GetHashCode());

        public static IEnumerable<object[]> GetEqualityTestCases()
        {
            yield return new object[] {One, One, true};
            yield return new object[] {Two, One, false};
            yield return new object[] {new Rational(1, 2), new Rational(8, 16), true};
            yield return new object[] {Half, One, false};
            yield return new object[] {new Rational(0, 2), new Rational(0, 1), true};
            yield return new object[] {new Rational(-1, -2), Half, true};
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

        [Theory]
        [MemberData(nameof(GetMultiplicationTestCases))]
        public void MultiplyTwoNumbers(Rational i, Rational j, Rational result)
            => Assert.Equal(result, i * j);

        public static IEnumerable<object[]> GetMultiplicationTestCases()
        {
            yield return new object[] {One, One, One};
            yield return new object[] {Zero, One, Zero};
            yield return new object[] {One, Zero, Zero};
            yield return new object[] {Zero, Zero, Zero};
            yield return new object[] {One, MinusOne, MinusOne};
            yield return new object[] {MinusOne, MinusOne, One};
            yield return new object[] {Two, Two, Four};
            yield return new object[] {Two, One, Two};
            yield return new object[] {Two, Four, new Rational(8)};
            yield return new object[] {new Rational(-2), Four, new Rational(-8)};
        }

        [Theory]
        [MemberData(nameof(GetDivisionTestCases))]
        public void DivideOneNumberByAnother(Rational i, Rational j, Rational result)
            => Assert.Equal(result, i / j);

        public static IEnumerable<object[]> GetDivisionTestCases()
        {
            yield return new object[] {One, One, One};
            yield return new object[] {One, Two, Half};
            yield return new object[] {One, Half, Two};
            yield return new object[] {Half, One, Half};
            yield return new object[] {MinusOne, MinusOne, One};
            yield return new object[] {new Rational(-8), Two, new Rational(-4)};
        }

        [Theory]
        [MemberData(nameof(GetPowerRaiseTestCases))]
        public void RaiseToPower(Rational i, int j, Rational result)
            => Assert.Equal(result, i.ToPower(j));

        public static IEnumerable<object[]> GetPowerRaiseTestCases()
        {
            yield return new object[] {One, 1, One};
            yield return new object[] {One, 423, One};
            yield return new object[] {MinusOne, 2, One};
            yield return new object[] {MinusOne, 3, MinusOne};
            yield return new object[] {Half, 2, new Rational(1, 4)};
            yield return new object[] {new Rational(10), 4, new Rational(10_000)};
            yield return new object[] {new Rational(1000), 0, One};
            yield return new object[] {One, -1, One};
            yield return new object[] {Two, -1, Half};
            yield return new object[] {Two, -3, new Rational(1, 8)};
            yield return new object[] {Half, -2, new Rational(4)};
        }

        [Theory]
        [MemberData(nameof(GetTryGetIntTestCases))]
        public void TryGetRationalAsInt(Rational i, int j, bool isInt)
        {
            Assert.Equal(isInt, i.TryGetInt(out var result));
            Assert.Equal(j, result);
        }

        public static IEnumerable<object[]> GetTryGetIntTestCases()
        {
            yield return new object[] {One, 1, true};
            yield return new object[] {Zero, 0, true};
            yield return new object[] {new Rational(400), 400, true};
            yield return new object[] {new Rational(400, 2), 200, true};
            yield return new object[] {new Rational(7, 2), 0, false};
            yield return new object[] {MinusOne, -1, true};
            yield return new object[] {new Rational(-7, 2), 0, false};
        }
    }
}
