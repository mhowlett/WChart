using System;
using System.Collections.Generic;
using System.Text;

namespace WChart
{
    public struct Dimensions
    {
        public Dimensions(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
        }
        private int _rows;

        public int Columns
        {
            get
            {
                return _columns;
            }
        }
        private int _columns;

    }
}
