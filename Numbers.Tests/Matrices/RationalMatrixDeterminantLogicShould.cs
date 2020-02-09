using System.Collections.Generic;
using Numbers;
using Numbers.Matrices;
using Xunit;

namespace Numbers.Matrices.Tests
{
    using R = Rational;
    using RM = RationalMatrix;
    using InvalidOperationException = RationalMatrix.InvalidOperationException;
    using static TestRationalMatrices;

    public class RationalMatrixDeterminantLogicShould
    {
        [Theory]
        [MemberData(nameof(GetTwoByTwoMatricesAndDeterminants))]
        [MemberData(nameof(GetThreeByThreeMatricesAndDeterminants))]
        public void GetDeterminantsOfMatrices(RM matrix, R determinant)
            => Assert.Equal(determinant, matrix.Determinant);

        public static IEnumerable<object[]> GetTwoByTwoMatricesAndDeterminants()
        {
            yield return new object[] {TwoByTwo, -2};
            yield return new object[] {new RM(A), 14};
            yield return new object[] {new RM(B), -2};
            yield return new object[] {new RM(new R[][] {new R[] {-5, 1}, new R[]{-2, 3}}), -13};
        }

        public static IEnumerable<object[]> GetThreeByThreeMatricesAndDeterminants()
        {
            yield return new object[] {new RM(new R[][] {new R[] {1, -3, 4}, new R[] {2, 0, 1}, new R[] {-1, 2, 1}}), 23};
            yield return new object[] {new RM(new R[][] {new R[] {1, -2, 3}, new R[] {0, -1, 2}, new R[] {-4, 0, 2}}), 2};
            yield return new object[] {new RM(new R[][] {new R[] {1, 0, 2}, new R[] {0, -1, -1}, new R[] {2, -1, 2}}), 1};
        }
    }
}