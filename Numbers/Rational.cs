namespace Numbers
{
    using static Functions;

    public struct Rational
    {
        public Rational(int numerator, int denominator)
        {
            if (denominator == 0) throw new System.DivideByZeroException();
            if (numerator == 0)
            {
                Numerator = 0;
                Denominator = 1;
            }
            else
            {
                var gcd = GCD(numerator, denominator);
                Numerator = numerator / gcd;
                Denominator = denominator / gcd;
            }
        }

        private int Numerator { get; }
        private int Denominator { get ; }

        public static bool operator ==(Rational first, Rational second)
            => (first.Numerator, first.Denominator) == (second.Numerator, second.Denominator);

        public static bool operator !=(Rational first, Rational second)
            => !(first == second);

        public override string ToString()
            => Denominator != 1 ?
                $"{Numerator}/{Denominator}" :
                Numerator.ToString();
    }
}
