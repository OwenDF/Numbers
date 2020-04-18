using System;

namespace Numbers
{
    using static Functions;
    using static Math;

    public readonly struct Rational
    {
        private static readonly Rational Zero = new Rational(0);
        
        public Rational(int integerValue)
        {
            Numerator = integerValue;
            Denominator = 1;
        }

        public Rational(int numerator, int denominator)
        {
            if (denominator == 0) throw new DivideByZeroException();

            if (denominator < 0) (numerator, denominator) = (numerator * -1, denominator * -1);
            
            if (numerator == 0) (Numerator, Denominator) = (0, 1);
            else
            {
                var gcd = GCD(numerator, denominator);
                (Numerator, Denominator) = (numerator / gcd, denominator / gcd);
            }
        }

        private int Numerator { get; }
        private int Denominator { get; }

        public static implicit operator double(Rational r) => ((double) r.Numerator) / r.Denominator;

        public static implicit operator Rational(int i) => new Rational(i);

        public static bool operator ==(Rational i, Rational j)
            => (i.Numerator, i.Denominator) == (j.Numerator, j.Denominator);

        public static bool operator !=(Rational i, Rational j) => !(i == j);

        public static Rational operator +(Rational i, Rational j)
        {
            var lcm = LCM(i.Denominator, j.Denominator);
            return new Rational(RaiseNumerator(i, lcm) + RaiseNumerator(j, lcm), lcm);
        }

        public static Rational operator -(Rational i, Rational j)
        {
            var lcm = LCM(i.Denominator, j.Denominator);
            return new Rational(RaiseNumerator(i, lcm) - RaiseNumerator(j, lcm), lcm);
        }

        public static Rational operator *(Rational i, Rational j)
            => new Rational(i.Numerator * j.Numerator, i.Denominator * j.Denominator);

        public static Rational operator /(Rational i, Rational j)
            => new Rational(i.Numerator * j.Denominator, i.Denominator * j.Numerator);

        public static Rational[] CreateZeroedArray(int length)
        {
            if (length < 1) throw new ArgumentException($"Array must have positive length, was given {length}", nameof(length));
            var array = new Rational[length];
            for (var i = 0; i < length; i++) array[i] = Zero;
            return array;
        }
        
        public static Rational[,] CreateZeroedArray(int firstDimension, int secondDimension)
        {
            if (firstDimension < 1) throw new ArgumentException($"Array must have positive size, was given {firstDimension}", nameof(firstDimension));
            if (secondDimension < 1) throw new ArgumentException($"Array must have positive size, was given {secondDimension}", nameof(secondDimension));
            
            var array = new Rational[firstDimension, secondDimension];
            for (var i = 0; i < firstDimension; i++)
            for (var j = 0; j < secondDimension; j++)
                array[i, j] = Zero;
            
            return array;
        }

        public Rational ToPower(int i)
            => i > 0 ?
                new Rational(Numerator.ToPower(i), Denominator.ToPower(i)) :
                new Rational(Denominator.ToPower(Abs(i)), Numerator.ToPower(Abs(i)));

        public bool TryGetInt(out int i)
        {
            if (Denominator == 1)
            {
                i = Numerator;
                return true;
            }
            i = 0;
            return false;
        }

        public override string ToString()
            => Denominator != 1 ?
                $"{Numerator}/{Denominator}" :
                Numerator.ToString();

        public override int GetHashCode()
            => (Numerator, Denominator).GetHashCode();

        public override bool Equals(object o)
            => o switch
            {
                Rational r => (this == r),
                int i => (this == i),
                _ => false
            };

        public bool Equals(Rational r) => this == r;

        private static int RaiseNumerator(Rational i, int newDenominator)
            => (i.Numerator * (newDenominator / i.Denominator));
    }
}
