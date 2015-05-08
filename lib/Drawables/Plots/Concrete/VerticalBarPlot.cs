
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace WChart
{

    /// <summary>
    /// Vertical Bars drawn from y=0 to ys.
    /// </summary>
    public class VerticalBarPlot : IPlot
    {
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xs">x values</param>
        /// <param name="ys">y values</param>
        public VerticalBarPlot(List<double> xs, List<List<double>> ys )
        {
            for (int i = 0; i < ys.Count; ++i)
            {
                if (xs.Count != ys[i].Count)
                {
                    throw new WChartException("xs and ys1 need to be same length");
                }
            }
            _xs = xs;
            _ys = ys;
        }


        /// <summary>
        /// The main draw function.
        /// </summary>
        /// <param name="dc">DrawingContext to draw on</param>
        /// <param name="hAxis">horizontal axis to draw against.</param>
        /// <param name="vAxis">vertical axis to draw against.</param>
        public void Draw( DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis )
        {
            // check that there are the correct number of fill brushes defined (or none at all).
            if (_fills != null)
            {
                if (_fills.Count != _ys.Count)
                {
                    throw new WChartException("Either the same number of Fills as y bars should be defined, or Fills should be set to null.");
                }
            }

            Pen p = new Pen(_stroke, _strokeThickness);
            for (int i = 0; i < _xs.Count; ++i)
            {
                if (_xs[i] < hAxis.Axis.WorldMin || _xs[i] > hAxis.Axis.WorldMax)
                {
                    continue;
                }

                double x1 = _xs[i] - BarWidth / 2.0;
                double x2 = _xs[i] + BarWidth / 2.0;

                int firstY = (int)vAxis.PhysicalMinY;
                int prevY = (int)vAxis.PhysicalMinY;
                for (int j = 0; j < _ys.Count; ++j)
                {
                    double y1 = _ys[j][i];

                    double px1 = hAxis.WorldToPhysical(x1, ClippingType.Clip);
                    double px2 = hAxis.WorldToPhysical(x2, ClippingType.Clip);
                    int currentY = (int)vAxis.WorldToPhysical(y1, ClippingType.NoClip) - firstY + prevY;

                    Brush fill = _defaultFill;
                    if (_fills != null)
                    {
                        fill = _fills[j];
                    }
                    dc.DrawRectangle(fill, p, new Rect((int)px1, currentY, (int)(px2 - px1), prevY - currentY));

                    if (_showValueText && y1 != 0.0)
                    {
                        FormattedText ft = new FormattedText(
                            y1.ToString(_valueTextFormat), CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                            new Typeface("Arial"), 13.0, Brushes.Black);

                        if (ft.Width < prevY - currentY + 2)
                        {
                            Point pp = new Point(-ft.Width/2.0, -ft.Height/2.0);
                            dc.PushTransform(new TranslateTransform((int) ((px1 + px2)/2.0),
                                                                    (int) ((prevY + currentY)/2.0)));
                            dc.PushTransform(new RotateTransform(-90));
                            dc.DrawText(ft, pp);
                            dc.Pop();
                            dc.Pop();
                        }
                    }
                    prevY = currentY;
                }
            }
        }


        /// <summary>
        /// not implemented
        /// </summary>
        /// <returns>not implemented</returns>
        public Axis SuggestedAxisX()
        {
            throw new Exception("The method or operation is not implemented.");
        }


        /// <summary>
        /// not implemented
        /// </summary>
        /// <returns>not implemented</returns>
        public Axis SuggestedAxisY()
        {
            if (_ys.Count == 0)
            {
                return null;
            }

            double maxSum = double.MinValue;
            for (int i = 0; i < _ys[0].Count; ++i)
            {
                double sum = 0.0;
                for (int j = 0; j < _ys.Count; ++j)
                {
                    sum += _ys[j][i];
                }
                if (sum > maxSum)
                {
                    maxSum = sum;
                }
            }
            return new LinearAxis(0.0, 1.1 * maxSum);
        }


        /// <summary>
        /// Bar width in world units.
        /// </summary>
        public double BarWidth
        {
            get
            {
                return _barWitdh;
            }
            set
            {
                _barWitdh = value;
            }
        }
        private double _barWitdh = 0.6;


        /// <summary>
        /// The brush used to fill the bars.
        /// </summary>
        public List<Brush> Fills
        {
            get
            {
                return _fills;
            }
            set
            {
                _fills = value;
            }
        }
        private List<Brush> _fills;
        private Brush _defaultFill = Brushes.LightBlue;


        /// <summary>
        /// The brush to use to draw the bar borders.
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return _stroke;
            }
            set
            {
                _stroke = value;
            }
        }
        private Brush _stroke = Brushes.Black;


        /// <summary>
        /// The thickness of the bar borders.
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
            set
            {
                _strokeThickness = value;
            }
        }
        private double _strokeThickness = 1.0;


        /// <summary>
        /// If true, text is drawn in the center of the bar corresponding to the bar height.
        /// </summary>
        public bool ShowValueText
        {
            get
            {
                return _showValueText;
            }
            set
            {
                _showValueText = value;
            }
        }
        private bool _showValueText = true;

        public string ValueTextFormat
        {
            get
            {
                return _valueTextFormat;
            }
            set
            {
                _valueTextFormat = value;
            }
        }
        private string _valueTextFormat = "0.0";

        /// <summary>
        /// x values
        /// </summary>
        private List<double> _xs;


        /// <summary>
        /// y values
        /// </summary>
        private List<List<double>> _ys;


        /// <summary>
        /// Labels for each of the y value lists.
        /// </summary>
        public List<string> Labels
        {
            set
            {
                _labels = value;
            }
            get
            {
                return _labels;
            }
        }
        private List<string> _labels;


        /// <summary>
        /// Adds an entry corresponding to this plot to the legend.
        /// </summary>
        /// <param name="legend">the legend to add representation of us to.</param>
        public void AddToLegend(Legend legend)
        {
            if (_labels != null)
            {
                // sanity check
                if (_labels.Count != _ys.Count || _labels.Count != _fills.Count)
                {
                    throw new WChartException("Expecting same number of labels as y lists");
                }

                for (int i = 0; i < _labels.Count; ++i)
                {
                    legend.AddItem(new LegendItem_FilledBox(_labels[i], _fills[i]));
                }
            }
        }

    }
}
