using System;
using System.Collections.Generic;
using System.Linq;

namespace Numbers.Matrices
{
    using R = Rational;
    using static Functions;

    public class RationalMatrix
    {
        private static readonly (int, int) TwoByTwo = (2, 2);
        private static readonly (int, int) OneByOne = (1, 1);

        private readonly int _rowCount;
        private readonly int _columnCount;
        private readonly R[,] _values;
        private readonly Lazy<RationalMatrix> _transpose;
        private readonly Lazy<R> _determinant;
        private readonly Lazy<RationalMatrix> _inverse;

        public RationalMatrix(IEnumerable<IEnumerable<R>> values):this()
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            
            var rows = values is IList<IEnumerable<R>> l ? l : values.ToList();

            _rowCount = rows.Count > 0 ?
                            rows.Count :
                            throw new ArgumentException("Matrix must contain at least one value");

            var firstRow = rows[0] is IList<R> l2 ? l2 : rows[0]?.ToList() ??
                           throw NullRowEx;

            _columnCount = firstRow.Count > 0 ? firstRow.Count : throw NullRowEx;

            _values = new R[_rowCount, _columnCount];

            for (var i = 1; i < _rowCount; i++)
            for (var j = 0; j < _columnCount; j++)
            {
                var row = rows[i] is IList<R> li ? li : rows[i]?.ToList() ??
                           throw NullRowEx;
                if (row.Count != _columnCount) throw new ArgumentException("All rows must be of the same length");

                _values[i, j] = row[j];
            }

            for (var j = 0; j < _columnCount; j++)
                _values[0, j] = firstRow[j];
        }

        private RationalMatrix(R[,] values, int rows, int columns):this()
        {
            _rowCount = rows;
            _columnCount = columns;
            _values = values;
        }

        private RationalMatrix(R[,] values, int rows, int columns, RationalMatrix? inverse = null, RationalMatrix? transpose = null):this(values, rows, columns)
        {
            if (inverse != null)
            {
                _inverse = new Lazy<RationalMatrix>(() => inverse);
                _determinant = new Lazy<R>(() => inverse._determinant.Value.ToPower(-1));
            }

            _transpose = transpose != null ? new Lazy<RationalMatrix>(() => transpose) : _transpose;
        }

        // General constructor, should be called from all others for field initialisations.
        private RationalMatrix()
        {
            _transpose = new Lazy<RationalMatrix>(TransposeMatrix);
            _inverse = new Lazy<RationalMatrix>(Invert);
            _determinant = new Lazy<R>(GetDeterminant);
        }

        public (int rows, int columns) Size => (_rowCount, _columnCount);
        public bool IsSquare => _rowCount == _columnCount;
        public RationalMatrix Transpose => _transpose.Value;
        public RationalMatrix Inverse => IsSingular ?
                                         throw new InvalidOperationException("Cannot invert singular matrix") :
                                         _inverse.Value;
        public bool IsSingular => IsSquare ?
                                  Determinant == 0 :
                                  throw new InvalidOperationException($"Non-square matrix does not have singularity property: {Size}");
        public R Determinant => IsSquare ?
                                _determinant.Value :
                                throw new InvalidOperationException($"Cannot compute determinant for non-square matrix: {Size}");

        private static ArgumentException NullRowEx => new ArgumentException("Cannot initialise with a null or empty row");

        public static bool operator ==(RationalMatrix? m, RationalMatrix? n)
            => ReferenceEquals(m, n) ||
               !(ReferenceEquals(m, null) ^ ReferenceEquals(n, null)) &&
               (m.Size == n.Size && (n.AsEnumerable(), m.AsEnumerable()).All((x, y) => x == y));

        public static bool operator !=(RationalMatrix? m, RationalMatrix? n) => !(m == n);

        public static RationalMatrix operator +(RationalMatrix m, RationalMatrix n)
        {
            if (m.Size != n.Size) throw new InvalidOperationException(GenerateSizeExceptionMessage("addition", m, n));

            var (rows, columns) = (m.Size.rows, m.Size.columns);
            var values = new R[rows, columns];

            for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
                values[i, j] = m._values[i, j] + n._values[i, j];

            return new RationalMatrix(values, rows, columns);
        }

        public static RationalMatrix operator *(RationalMatrix m, RationalMatrix n)
        {
            if (m.Size.columns != n.Size.rows)
                throw new InvalidOperationException(GenerateSizeExceptionMessage("multiplication", m, n));

            var (rows, columns) = (m.Size.rows, n.Size.columns);
            var values = new R[rows, columns];

            for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
                values[i, j] = (m.RowAsEnumerable(i), n.ColumnAsEnumerable(j)).Select((x, y) => x * y).Sum();

            return new RationalMatrix(values, rows, columns);
        }

        public static RationalMatrix operator *(RationalMatrix m, Rational scalar)
        {
            var (rows, columns) = (m.Size.rows, m.Size.columns);
            var values = new R[rows, columns];

            for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
                values[i, j] = m._values[i, j] * scalar;

            return new RationalMatrix(values, rows, columns);
        }

        public static RationalMatrix operator *(Rational scalar, RationalMatrix m) => m * scalar;

        public static RationalMatrix CreateDiagonalMatrix(IEnumerable<R> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            
            var valueList = values is IList<R> l ? l : values.ToList();
            var size = valueList.Count > 0 ?
                valueList.Count :
                throw new ArgumentException("Matrix must contain at least one value");

            var matrixValues = R.CreateZeroedArray(size, size);
            for (var i = 0; i < size; i++) matrixValues[i, i] = valueList[i];
            return new RationalMatrix(matrixValues, size, size);
        }

        // This is temporary, to be replaced at some point
        public override string ToString()
        {
            var matrixString = string.Empty;
            
            for (var i = 0; i < _rowCount; i++)
            {
                matrixString = matrixString + "[ ";
                for (var j = 0; j < _columnCount; j++)
                {
                    matrixString = matrixString + $"{_values[i, j]} ";
                }
                matrixString = matrixString + "]\n";
            }

            return matrixString;
        }

        public RationalMatrix ToPower(int exponent)
        {
            if (exponent < 1) throw new ArgumentException("Cannot raise to a non-positive power using this method", nameof(exponent));

            var (powerBase, result) = (this, this);

            for (var count = 1; count < exponent; count++) result *= powerBase;

            return result;
        }

        public override bool Equals(object obj)
            => obj is RationalMatrix rm && Equals(rm);

        public bool Equals(RationalMatrix rm)
            => this == rm;

        public override int GetHashCode()
            => AsEnumerable().Aggregate(352033288, (current, r) => current * -1521134295 + r.GetHashCode());

        private static string GenerateSizeExceptionMessage(string operationName, RationalMatrix first, RationalMatrix second)
            => $"Mismatched matrices for {operationName}: {first.Size.rows}x{first.Size.columns} vs {second.Size.rows}x{second.Size.columns}";

        private RationalMatrix TransposeMatrix()
        {
            var (rows, columns) = (Size.columns, Size.rows);
            var values = new R[rows, columns];

            for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
                values[i, j] = _values[j, i];
            
            return new RationalMatrix(values, rows, columns, transpose: this);
        }

        private R GetDeterminant()
        {
            if (Size == OneByOne) return _values[0,0];
            if (Size == TwoByTwo) return (_values[0,0] * _values[1, 1]) - (_values[0, 1] * _values[1, 0]);

            Rational determinant = 0;
            for (var i = 0; i < _rowCount; i++)
                determinant += GetComplementaryValue(0, i) * _values[0, i];
            
            return determinant;
        }

        private RationalMatrix Invert()
        {
            if (Size == OneByOne) return new RationalMatrix(new R[,] {{_values[0,0].ToPower(-1)}}, 1, 1, this);

            var values = new R[_rowCount, _columnCount];
            for (var i = 0; i < _rowCount; i++)
            for (var j = 0; j < _columnCount; j++)
                values[i, j] = GetComplementaryValue(i, j);

            return (new RationalMatrix(values, _rowCount, _columnCount).Transpose) * _determinant.Value.ToPower(-1);
        }

        private R GetComplementaryValue(int row, int column)
            =>  GetSubMatrix(row, column).Determinant * (-1).ToPower(2 + row + column);

        private RationalMatrix GetSubMatrix(int rowToSkip, int columnToSkip)
        {
            var (rows, columns) = (_rowCount - 1, _columnCount - 1);
            var values = new R[rows, columns];

            var k = 0;

            for (var i = 0; i < _rowCount; i++)
            {
                var l = 0;
                if (i == rowToSkip) continue;
                for (var j = 0; j < _columnCount; j++)
                {
                    if (j == columnToSkip) continue;
                    values[k, l++] = _values[i, j];
                }
                k++;
            }

            return new RationalMatrix(values, rows, columns);
        }

        private IEnumerable<R> AsEnumerable()
        {
            for (var i = 0; i < _rowCount; i++)
            for (var j = 0; j < _columnCount; j++)
            {
                yield return _values[i, j];
            }
        }

        private IEnumerable<R> RowAsEnumerable(int row)
        {
            for (var i = 0; i < _columnCount; i++)
                yield return _values[row, i];
        }

        private IEnumerable<R> ColumnAsEnumerable(int column)
        {
            for (var i = 0; i < _rowCount; i++)
                yield return _values[i, column];
        }

        public class InvalidOperationException : Exception
        {
            public InvalidOperationException(string msg): base(msg) {}
        }
    }
}