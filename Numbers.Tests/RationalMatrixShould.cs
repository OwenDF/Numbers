using System;
using Numbers;
using Xunit;

namespace Numbers.Tests
{
    using R = Rational;
    using RM = RationalMatrix;

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
    }
}