using System;
using System.Collections.Generic;
using Numbers;
using Xunit;

namespace Numbers.Tests
{
    using R = Rational;
    using RM = RationalMatrix;
    using InvalidOperationException = RationalMatrix.InvalidOperationException;
    using static TestRationalMatrices;

    public class RationalMatrixShould
    {
        [Fact]
        public void ThrowForNullFirstEnumerable()
            => Assert.Throws<ArgumentNullException>(() => new RM(null));
        
        [Fact]
        public void ThrowForNullInnerEnumerable()
            => Assert.Throws<ArgumentException>(() => new RM(new R[][] {null}));
        
        [Fact]
        public void ThrowForEmptyFirstEnumerable()
            => Assert.Throws<ArgumentException>(() => new RM(new R[0][]));

        [Fact]
        public void ThrowIfFirstRowEmpty()
            => Assert.Throws<ArgumentException>(() => new RM(new R[][] {new R[0]}));

        [Fact]
        public void ThrowIfDifferentLengthRows()
            => Assert.Throws<ArgumentException>(() => new RM(new R[][] {new R[2], new R[2], new R[1]}));

        [Fact]
        public void ReturnsCorrectRowCount()
            => Assert.Equal(2, new RM(new R[][] {new R[] {1}, new R[] {1}}).Size.rows);

        [Fact]
        public void ReturnCorrectColumnCount()
            => Assert.Equal(2, new RM(new R[][] {new R[] {1, 2}}).Size.columns);

        [Fact]
        public void CorrectlyInitialiseMatrix()
        {
            var input = new R[][] { new R[] {1, 2, 3}, new R[] {4, 5, 6}};
            const string expectedMatrix = "[ 1 2 3 ]\n[ 4 5 6 ]\n";

            Assert.Equal(expectedMatrix, new RM(input).ToString());
        }

        [Fact]
        public void ImplementEqualityCheck()
            => Assert.True(TwoByTwo == TwoByTwo);

        [Fact]
        public void ShowInequalityForDifferentSizedMatrices()
            => Assert.False(TwoByTwo == TwoByThree);
        
        [Fact]
        public void ShowInequalityForSameSizedMatrices()
            => Assert.False(TwoByTwo == new RM(new R[][] {new R[] {1, 2}, new R[] {5, 6}}));

        [Fact]
        public void OverrideEqualsMethod()
            => Assert.Equal((object)TwoByTwo, (object)TwoByTwo);

        [Theory]
        [MemberData(nameof(GetHashCodeTestCases))]
        public void OverrideGetHashCodeMethod(bool areEqual, RM m, RM n)
            => Assert.Equal(areEqual, m.GetHashCode() ==  n.GetHashCode());

        public static IEnumerable<object[]> GetHashCodeTestCases()
        {
            yield return new object[] {true, TwoByTwo, TwoByTwo};
            yield return new object[] {false, TwoByThree, TwoByTwo};
            yield return new object[] {false, TwoByTwo, new RM(new R[][] {new R[] {1, 2}, new R[] {4, 3}})};
        }

        [Fact]
        public void OnlyAllowAdditionOfEqualSizedMatrices()
            => Assert.Throws<InvalidOperationException>(() => TwoByTwo + TwoByThree);

        [Fact]
        public void AddTwoMatrices()
            => Assert.Equal(new RM(new R[][] {new R[] {2, 4}, new R[] {6, 8}}), TwoByTwo + TwoByTwo);

        [Fact]
        public void OnlyAllowMultiplicationOfCompatibleMatrices()
            => Assert.Throws<InvalidOperationException>(() => TwoByTwo * ThreeByTwo);

        [Theory]
        [MemberData(nameof(GetMultiplicationTestCases))]
        public void MultiplyTwoMatrices(RationalMatrix result, RationalMatrix m, RationalMatrix n)
            => Assert.Equal(result, m * n);

        public static IEnumerable<object[]> GetMultiplicationTestCases()
        {
            yield return new object[] {new RM(new R[][] {new R[] {7, 10}, new R[] {15, 22}}), TwoByTwo, TwoByTwo};

            // My assignment:
            yield return new object[] {new RM(AB), new RM(A), new RM(B)};
            yield return new object[] {new RM(CD), new RM(C), new RM(D)};
            yield return new object[] {new RM(AD), new RM(A), new RM(D)};
        }
    }
}