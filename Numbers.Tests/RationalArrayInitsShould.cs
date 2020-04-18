using System;
using System.Linq;
using Xunit;

namespace Numbers.Tests
{
    using static Rational;
    
    public class RationalArrayInitsShould
    {
        private static readonly Rational Zero = new Rational(0);
        
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ThrowExceptionForInvalidLength(int length)
        {
            var ex = Assert.Throws<ArgumentException>(() => CreateZeroedArray(length));
            Assert.Equal("length", ex.ParamName);
            Assert.Equal($"Array must have positive length, was given {length} (Parameter 'length')", ex.Message);
        }

        [Fact]
        public void InitialiseArrayWithAllZeroes()
            => Assert.True(CreateZeroedArray(20).All(x => x == Zero));

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ThrowExceptionForInvalidFirstDimension(int dimension)
        {
            var ex = Assert.Throws<ArgumentException>(() => CreateZeroedArray(dimension, 0));
            Assert.Equal("firstDimension", ex.ParamName);
            Assert.Equal($"Array must have positive size, was given {dimension} (Parameter 'firstDimension')", ex.Message);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-120)]
        public void ThrowExceptionForInvalidSecondDimension(int dimension)
        {
            var ex = Assert.Throws<ArgumentException>(() => CreateZeroedArray(1, dimension));
            Assert.Equal("secondDimension", ex.ParamName);
            Assert.Equal($"Array must have positive size, was given {dimension} (Parameter 'secondDimension')", ex.Message);
        }
        
        [Fact]
        public void Initialise2DArrayWithAllZeroes()
            => Assert.True(CreateZeroedArray(20, 5).Cast<Rational>().All(x => x == Zero));
    }
}