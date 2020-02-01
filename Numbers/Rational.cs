namespace Numbers
{
    using static Functions;

    public struct Rational
    {
        public Rational(int i)
        {
            Numerator = i;
            Denominator = 1;
        }

        public Rational(int numerator, int denominator)
        {
            if (denominator == 0) throw new System.DivideByZeroException();

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

        public static bool operator ==(Rational i, Rational j)
            => (i.Numerator, i.Denominator) == (j.Numerator, j.Denominator);

        public static bool operator !=(Rational i, Rational j)
            => !(i == j);

        public static Rational operator +(Rational i, Rational j)
        {
            var lcm = LCM(i.Denominator, j.Denominator);
            return new Rational((i.Numerator * (lcm / i.Denominator)) + (j.Numerator * (lcm / j.Denominator)), lcm);
        }

        public static Rational operator -(Rational i, Rational j)
        {
            var lcm = LCM(i.Denominator, j.Denominator);
            return new Rational((i.Numerator * (lcm / i.Denominator)) - (j.Numerator * (lcm / j.Denominator)), lcm);
        }

        public override string ToString()
            => Denominator != 1 ?
                $"{Numerator}/{Denominator}" :
                Numerator.ToString();
    }
}
