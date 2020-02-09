using Numbers;
using Numbers.Matrices;
using Xunit;

namespace Numbers.Matrices.Tests
{
    using R = Rational;
    using RM = RationalMatrix;
    using static TestRationalMatrices;

    public class RationalMatrixTranspositionShould
    {
        [Fact]
        public void CorrectlyTransposeMatrix()
            => Assert.Equal(new RM(new R[][] {new R[] {1, 3}, new R[] {2, 4}}), TwoByTwo.Transpose);

        [Fact]
        public void DoMyHomework()
            => Assert.Equal(new RM( new R[][] {new R[] {1, -1, 4}, new R[] {-3, 2, 1}}), new RM(C).Transpose);

        [Fact]
        public void TransposeOfTransposeIsOriginal()
            => Assert.True(ThreeByThree.Transpose.Transpose == ThreeByThree);
    }
}