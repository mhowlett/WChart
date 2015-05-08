
// (c) 2007 Matthew Howlett

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WChart
{

    /// <summary>
    /// Doesn't plot data as such - draws grid lines.
    /// </summary>
    public class GridPlot : IPlot
    {

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="dc">DrawingContext on which to draw.</param>
        /// <param name="hAxis">horizontal axis to draw against.</param>
        /// <param name="vAxis">vertical axis to draw against.</param>
        public void Draw( DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis )
        {
            Pen minorPen = new Pen(_stroke_minor, _strokeThickness_minor);
            Pen majorPen = new Pen(_stroke_major, _strokeThickness_major);

            if (_visibility_horizontal == Visibility.Visible)
            {
                List<AxisMarking> hTicks = hAxis.GetAxisMarkings();

                for (int i = 0; i < hTicks.Count; ++i)
                {
                    Pen p = minorPen;
                    if (hTicks[i].TickType == TickType.Large)
                    {
                        p = majorPen;
                    }

                    double x = hAxis.WorldToPhysical(hTicks[i].World, ClippingType.Clip);

                    dc.DrawLine(p, new Point(x, vAxis.PhysicalMinY), new Point(x, vAxis.PhysicalMaxY));
                }
            }

            if (_visibility_vertical == Visibility.Visible)
            {
                List<AxisMarking> vTicks = vAxis.GetAxisMarkings();

                for (int i = 0; i < vTicks.Count; ++i)
                {
                    Pen p = minorPen;
                    if (vTicks[i].TickType == TickType.Large)
                    {
                        p = majorPen;
                    }

                    double y = vAxis.WorldToPhysical(vTicks[i].World, ClippingType.Clip);

                    dc.DrawLine(p, new Point((int)hAxis.PhysicalMinX, (int)y), new Point((int)hAxis.PhysicalMaxX, (int)y));
                }
            }

        }


        /// <summary>
        /// Grid lines suggest no axes.
        /// </summary>
        /// <returns>null</returns>
        public Axis SuggestedAxisX()
        {
            return null;
        }


        /// <summary>
        /// Grid lines suggest no axes.
        /// </summary>
        /// <returns>null</returns>
        public Axis SuggestedAxisY()
        {
            return null;
        }


        /// <summary>
        /// The brush to use for lines drawn against large ticks.
        /// </summary>
        public Brush Stroke_Major
        {
            get
            {
                return _stroke_major;
            }
            set
            {
                _stroke_major = value;
            }
        }
        private Brush _stroke_major = Brushes.LightGray;


        /// <summary>
        /// The thickness of the line to draw against large ticks.
        /// </summary>
        public double StrokeThickness_Major
        {
            get
            {
                return _strokeThickness_major;
            }
            set
            {
                _strokeThickness_major = value;
            }
        }
        private double _strokeThickness_major = 1.0;


        /// <summary>
        /// The brush to use to draw against small ticks.
        /// </summary>
        public Brush Stroke_Minor
        {
            get
            {
                return _stroke_minor;
            }
            set
            {
                _stroke_minor = value;
            }
        }
        private Brush _stroke_minor = Brushes.LightGray;


        /// <summary>
        /// The thickness of the brush to use to draw against small ticks.
        /// </summary>
        public double StrokeThickness_Minor
        {
            get
            {
                return _strokeThickness_minor;
            }
            set
            {
                _strokeThickness_minor = value;
            }
        }
        private double _strokeThickness_minor = 1.0;


        /// <summary>
        /// Whether or not to draw grid lines associated with the horizontal axis.
        /// </summary>
        public Visibility Visibility_Horizontal
        {
            get
            {
                return _visibility_horizontal;
            }
            set
            {
                _visibility_horizontal = value;
            }
        }
        private Visibility _visibility_horizontal = Visibility.Visible;


        /// <summary>
        /// Whether or not to draw grid lines associated with the vertical axis.
        /// </summary>
        public Visibility Visibility_Vertical
        {
            get
            {
                return _visibility_vertical;
            }
            set
            {
                _visibility_vertical = value;
            }
        }
        private Visibility _visibility_vertical = Visibility.Visible;


        /// <summary>
        /// Adds an entry corresponding to this plot to the legend.
        /// </summary>
        /// <param name="legend">the legend to add representation of us to.</param>
        /// <remarks>Does nothing</remarks>
        public void AddToLegend(Legend legend)
        {
            // do nothing.
        }

    }
}
