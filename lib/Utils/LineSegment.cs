using System;
using System.Collections.Generic;
using System.Text;

namespace WChart
{

    public struct LineSegment
    {
        public LineSegment(double x1, double y1, double x2, double y2)
        {
            _x1 = x1;
            _x2 = x2;
            _y1 = y1;
            _y2 = y2;
        }

        public double X1
        {
            get
            {
                return _x1;
            }
        }
        private double _x1;

        public double X2
        {
            get
            {
                return _x2;
            }
        }
        private double _x2;

        public double Y1
        {
            get
            {
                return _y1;
            }
        }
        private double _y1;

        public double Y2
        {
            get
            {
                return _y2;
            }
        }
        private double _y2;
    }

}
