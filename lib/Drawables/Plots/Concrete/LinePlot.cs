
// (c) 2007 Matthew Howlett

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WChart
{

    /// <summary>
    /// Allows plotting of series of points as a line.
    /// </summary>
	public class LinePlot : IPlot
	{
	
        /// <summary>
        /// The x values.
        /// </summary>
		private List<double> _xs;

        /// <summary>
        /// The y values
        /// </summary>
		private List<double> _ys;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xs">x values of line to plot</param>
        /// <param name="ys">y values of line to plot</param>
		public LinePlot( List<double> xs, List<double> ys )
		{
			_xs = xs;
			_ys = ys;
		}


        /// <summary>
        /// An axis suitable for x extent of this plot.
        /// </summary>
        /// <returns>An axis suitable for x extent of this plot.</returns>
		public Axis SuggestedAxisX()
		{
			double minX = _xs[0];
			double maxX = _xs[0];
			for (int i = 0; i < _xs.Count; ++i)
			{
				if (_xs[i] > maxX)
				{
					maxX = _xs[i];
				}
				if (_xs[i] < minX)
				{
					minX = _xs[i];
				}
			}
			return new LinearAxis(minX, maxX);
		}


        /// <summary>
        /// An axis suitable for y extent of this plot.
        /// </summary>
        /// <returns>An axis suitable for y extent of this plot.</returns>
		public Axis SuggestedAxisY()
		{
			double minY = _ys[0];
			double maxY = _ys[0];
			for (int i = 0; i < _ys.Count; ++i)
			{
				if (_ys[i] > maxY)
				{
					maxY = _ys[i];
				}
				if (_ys[i] < minY)
				{
					minY = _ys[i];
				}
			}
			return new LinearAxis(minY, maxY);
		}


        /// <summary>
        /// Brush to use to draw the line.
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
        private Brush _stroke;


        /// <summary>
        /// Thickness of the line.
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
        private double _strokeThickness;


		public void Draw( DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis )
		{
            Pen pen = new Pen(_stroke, _strokeThickness);

			for (int i = 0; i < _xs.Count - 1; ++i)
			{
				double x1 = hAxis.WorldToPhysical(_xs[i], ClippingType.Clip);
				double x2 = hAxis.WorldToPhysical(_xs[i+1], ClippingType.Clip);
				double y1 = vAxis.WorldToPhysical(_ys[i], ClippingType.Clip);
				double y2 = vAxis.WorldToPhysical(_ys[i + 1], ClippingType.Clip);

				dc.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
			}
		}


        /// <summary>
        /// Adds an entry corresponding to this plot to the legend.
        /// </summary>
        /// <param name="legend">the legend to add representation of us to.</param>
        public void AddToLegend(Legend legend)
        {
            legend.AddItem(new LegendItem_Line(_label, _stroke, _strokeThickness));
        }


        /// <summary>
        /// Label for the plot - used by legend.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }
        private string _label = null;

    }

}
