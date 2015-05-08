
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text;
using System.Globalization;

namespace WChart
{

    /// <summary>
    /// A wrapper around an axis which adds physical extents and drawing functionality for vertical axes.
    /// </summary>
	public class VerticalPhysicalAxis : PhysicalAxis
	{

        /// <summary>
        /// Should never be called.
        /// </summary>
        private VerticalPhysicalAxis()
            : base(null)
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="axis">The axis wrapped by this physical axis.</param>
        /// <param name="physicalMinY">The minimum physical Y of the axis.</param>
        /// <param name="physicalMaxY">The maximum physical Y of the axis.</param>
        /// <param name="physicalX">The physical X of the axis.</param>
        public VerticalPhysicalAxis(Axis axis, double physicalMinY, double physicalMaxY, double physicalX )
            : base(axis)
        {
            _physicalMinY = physicalMinY;
            _physicalMaxY = physicalMaxY;
            _physicalX = physicalX;
        }
		

		/// <summary>
		/// The orientation of the ticks (must be either up or down).
		/// </summary>
		public TickOrientation TickOrientation
		{
			get
			{
				return _tickOrientation;
			}
			set
			{
				if (value == TickOrientation.Up || value == TickOrientation.Down)
				{
					throw new WChartException("Tick orientation on horizontal axis must be either up or down.");
				}
				_tickOrientation = value;
			}
		}
		private TickOrientation _tickOrientation = TickOrientation.Right;


        /// <summary>
        /// Draws the axis
        /// </summary>
        /// <param name="dc">DrawingContext to draw with</param>
		public void Draw(DrawingContext dc)
		{
            if (_axis == null)
            {
                return;
            }

			double multiplier = 1;
			if (_tickOrientation == TickOrientation.Left)
			{
				multiplier = -1;
			}

            Pen pen = new Pen(_stroke, _strokeThickness);

            // make sure that all lines are on integer boundaries, to ensure sharp lines (gridlines set to 0.5, line width 1).
			dc.DrawLine(pen, new Point((int)_physicalX, (int)_physicalMinY), new Point((int)_physicalX, (int)_physicalMaxY));
			List<AxisMarking> ticks = _axis.GetAxisMarkings(_physicalMinY, _physicalMaxY);

            double maxTickTextWidth = 0.0;
			foreach (AxisMarking tick in ticks)
			{
				double offset = 1.0;
				offset *= multiplier;
				switch (tick.TickType)
				{
					case TickType.Large:
						offset *= LargeTickLength;
						break;
					case TickType.Small:
						offset *= SmallTickLength;
						break;
					case TickType.None:
						offset *= 0.0;
						break;
					default:
						throw new Exception("Unknown tick type");
				}

				double physical = _axis.WorldToPhysical(tick.World, _physicalMinY, _physicalMaxY, ClippingType.NoClip);
                // make sure that all lines are on integer boundaries, to ensure sharp lines (gridlines set to 0.5, line width 1).
				
                dc.DrawLine(pen,
					new Point((int)_physicalX, (int)physical ),
					new Point((int)(_physicalX + offset), (int)physical));

                if (_visibility_textMarkings == Visibility.Visible)
                {
					if (tick.Text != null)
					{
						FormattedText ft = new FormattedText(
							tick.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
							new Typeface("Arial"), 12.0, Brushes.Black);

						if (ft.Width > maxTickTextWidth)
						{
							maxTickTextWidth = ft.Width;
						}

						double xPos = _physicalX - ft.Width - 10;
						double yPos = physical - ft.Height / 2.0;
						if (_tickOrientation == TickOrientation.Left)
						{
							xPos = _physicalX + 10;
						}

						dc.DrawText(ft, new Point(xPos, yPos));
					}
                }
			}

            if (this.Label != null)
            {
                FormattedText ft = new FormattedText(
                    this.Label, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 12.0, Brushes.Black);

                double xPos = _physicalX - ft.Height/2.0 - LabelExtraOffset - maxTickTextWidth;
                double yPos = (_physicalMinY + _physicalMaxY) / 2.0;
                if (_tickOrientation == TickOrientation.Left)
                {
                    xPos = _physicalX + ft.Height / 2.0 + LabelExtraOffset + maxTickTextWidth;
                }

                dc.PushTransform(new TranslateTransform(xPos, yPos));
                if (_tickOrientation == TickOrientation.Right)
                {
                    dc.PushTransform(new RotateTransform(-90.0));
                }
                else
                {
                    dc.PushTransform(new RotateTransform(90.0));
                }
                dc.DrawText(ft, new Point(-ft.Width/2.0,-ft.Height/2.0));
                dc.Pop();
                dc.Pop();
            }

		}


        /// <summary>
        /// Minimum physical y of the axis.
        /// </summary>
        public double PhysicalMinY
        {
            get
            {
                return _physicalMinY;
            }
            set
            {
                _physicalMinY = value;
            }
        }
        private double _physicalMinY;


        /// <summary>
        /// Maximum physical y of the axis.
        /// </summary>
        public double PhysicalMaxY
        {
            get
            {
                return _physicalMaxY;
            }
            set
            {
                _physicalMaxY = value;
            }
        }
        private double _physicalMaxY;


        /// <summary>
        /// Physical X of the axis.
        /// </summary>
        public double PhysicalX
        {
            get
            {
                return _physicalX;
            }
            set
            {
                _physicalX = value;
            }
        }
        private double _physicalX;


        /// <summary>
        /// Gets all axis markings. 
        /// </summary>
        /// <returns></returns>
        public override List<AxisMarking> GetAxisMarkings()
        {
            return _axis.GetAxisMarkings(_physicalMinY, _physicalMaxY);
        }


        /// <summary>
        /// Returns the physical coordinate corresponding to the provided world coordinate.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="clippingType"></param>
        /// <returns></returns>
        public override double WorldToPhysical(double world, ClippingType clippingType )
        {
            return _axis.WorldToPhysical(world, _physicalMinY, _physicalMaxY, clippingType); 
        }


        /// <summary>
        /// Returns the world coordinate corresponding to the provided physical coordinate.
        /// </summary>
        /// <param name="physical"></param>
        /// <param name="clippingType"></param>
        /// <returns></returns>
        public override double PhysicalToWorld(double physical, ClippingType clippingType)
        {
            return _axis.PhysicalToWorld(physical, _physicalMinY, _physicalMaxY, clippingType);
        }

	}
}
