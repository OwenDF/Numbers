using System.Collections.Generic;
using Xunit;

namespace Numbers.Matrices.Tests
{
    using R = Rational;
    using RM = RationalMatrix;
    using InvalidOperationException = RationalMatrix.InvalidOperationException;
    using static TestRationalMatrices;

    public class RationalMatrixInversionShould
    {
        [Fact]
        public void DescribeANonSingularMatrix()
            => Assert.False(TwoByTwo.IsSingular);

        [Fact]
        public void DescribeASingularMatrix()
            => Assert.True(new RM(new R[][] {new R[] {2, 6}, new R[] {1, 3}}).IsSingular);

        [Fact]
        public void ThrowExceptionInvertingSingularMatrix()
            => Assert.Throws<InvalidOperationException>(() => new RM(CD).Inverse);

        [Fact]
        public void InvertAOneByOneMatrix()
            => Assert.Equal(new RM(new R[][] {new R[] {2}}), new RM(new R[][] {new R[] {new R(1, 2)}}).Inverse);

        [Theory]
        [MemberData(nameof(GetTwoByTwoInversionTestCases))]
        [MemberData(nameof(GetThreeByThreeInversionTestCases))]
        public void InvertAMatrix(RM m, RM result)
            => Assert.Equal(result, m.Inverse);

        public static IEnumerable<object[]> GetTwoByTwoInversionTestCases()
        {
            yield return new object[] {TwoByTwo, new RM(new R[][] {new R[] {-2, 1}, new R[] {new R(3, 2), new R(-1, 2)}})};
            yield return new object[] {new RM(Q), new RM(QInv)};
        }

        public static IEnumerable<object[]> GetThreeByThreeInversionTestCases()
        {
            yield return new object[] {ThreeByThree, ThreeByThreeInv};
            yield return new object[] {new RM(r), new RM(RInv)};
        }

        [Theory]
        [MemberData(nameof(GetInverseByOriginalIsIdentityTestCases))]
        public void InverseMultipliedByOriginalEqualsIdentity(RM original, RM inverse, RM identity)
            => Assert.Equal(identity, original * inverse);

        public static IEnumerable<object[]> GetInverseByOriginalIsIdentityTestCases()
        {
            yield return new object[] {TwoByTwo, TwoByTwo.Inverse, TwoByTwoIdentity};
            yield return new object[] {ThreeByThree, ThreeByThree.Inverse, ThreeByThreeIdentity};
            yield return new object[] {new RM(r), new RM(RInv), ThreeByThreeIdentity};
        }

        [Fact]
        public void InverseOfInverseIsOriginal()
            => Assert.True(TwoByTwo.Inverse.Inverse == TwoByTwo);
    }
}