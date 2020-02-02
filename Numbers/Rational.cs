using System;

namespace Numbers
{
    using static Functions;
    using static Math;

    public struct Rational
    {
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
        private int Denominator { get ; }

        public static implicit operator double(Rational r) => ((double) r.Numerator) / ((double) r.Denominator);

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
        {
            var lcm = LCM(i.Denominator, j.Denominator);
            return new Rational(RaiseNumerator(i, lcm) * RaiseNumerator(j, lcm), lcm);
        }

        public static Rational operator /(Rational i, Rational j)
            => new Rational(i.Numerator * j.Denominator, i.Denominator * j.Numerator);

        public Rational ToPower(int i)
            => i > 0 ?
                new Rational(this.Numerator.ToPower(i), this.Denominator.ToPower(i)) :
                new Rational(this.Denominator.ToPower(Abs(i)), this.Numerator.ToPower(Abs(i)));

        public override string ToString()
            => Denominator != 1 ?
                $"{Numerator}/{Denominator}" :
                Numerator.ToString();

        private static int RaiseNumerator(Rational i, int newDenominator)
            => (i.Numerator * (newDenominator / i.Denominator));
    }
}
