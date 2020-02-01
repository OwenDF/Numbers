using System;
using System.Collections.Generic;
using Xunit;

namespace Numbers.Tests
{
    public class RationalShould
    {
        [Fact]
        public void CorrectlyOutputASimpleRationalAsString()
            => Assert.Equal("1/2", new Rational(1, 2).ToString());

        [Fact]
        public void CorrectlyOutputAnIntegerRationalAsString()
            => Assert.Equal("5", new Rational(5, 1).ToString());

        [Fact]
        public void SimplifyUponConstruction()
            => Assert.Equal("1/2", new Rational(2, 4).ToString());

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(100)]
        public void CorrectlyOutputZeroRationalAsString(int i)
            => Assert.Equal("0", new Rational(0, i).ToString());

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