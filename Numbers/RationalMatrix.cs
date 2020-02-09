using System;
using System.Collections.Generic;
using System.Linq;

namespace Numbers
{
    using R = Rational;

    public class RationalMatrix
    {
        private readonly int _rowCount;
        private readonly int _columnCount;
        private readonly R[,] _values;

        public RationalMatrix(IEnumerable<IEnumerable<R>> values)
        {
            var rows = values is IList<IEnumerable<R>> l ? l : values.ToList() ??
                       throw new ArgumentNullException(nameof(values));

            _rowCount = rows.Count > 0 ?
                            rows.Count :
                            throw new ArgumentException("Matrix must have at least one row");

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

        private RationalMatrix(R[,] values, int rows, int columns)
        {
            _rowCount = rows;
            _columnCount = columns;
            _values = values;
        }

        public (int rows, int columns) Size => (_rowCount, _columnCount);
        private ArgumentException NullRowEx => new ArgumentException("Cannot initialise with a null or empty row");

        public static bool operator ==(RationalMatrix m, RationalMatrix n)
            => m.Size == n.Size ? (n.AsEnumerable(), m.AsEnumerable()).All((x, y) => x == y) : false;

        public static bool operator !=(RationalMatrix m, RationalMatrix n) => !(m == n);

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

        public override bool Equals(object obj)
            => obj is RationalMatrix rm ? Equals(rm) : false;

        public bool Equals(RationalMatrix rm)
            => this == rm;

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            foreach (var r in AsEnumerable())
                hashCode = hashCode * -1521134295 + r.GetHashCode();

            return hashCode;
        }

        private static string GenerateSizeExceptionMessage(string operationName, RationalMatrix first, RationalMatrix second)
            => $"Mismatched matrices for {operationName}: {first.Size.rows}x{first.Size.columns} vs {second.Size.rows}x{second.Size.columns}";

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