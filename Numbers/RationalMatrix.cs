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

        public (int rows, int columns) Size => (_rowCount, _columnCount);

        private ArgumentException NullRowEx => new ArgumentException("Cannot initialise with a null or empty row");

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
    }
}