
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace WChart
{
	/// <summary>
	/// A wrapper around an axis which adds physical extents and drawing functionality for horizontal axes.
	/// </summary>
	public class HorizontalPhysicalAxis : PhysicalAxis
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="axis">The axis to base drawing on.</param>
		public HorizontalPhysicalAxis(Axis axis, double physicalMinX, double physicalMaxX, double physicalY)
            : base(axis)
		{
            _physicalMinX = physicalMinX;
            _physicalMaxX = physicalMaxX;
            _physicalY = physicalY;
		}

        /// <summary>
        /// Constructor should never be called.
        /// </summary>
		private HorizontalPhysicalAxis()
            : base(null)
		{
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
				if (value == TickOrientation.Left || value == TickOrientation.Right)
				{
					throw new WChartException("Tick orientation on horizontal axis must be either up or down.");
				}
				_tickOrientation = value;
			}
		}
		private TickOrientation _tickOrientation;

        /// <summary>
		/// Draw a horizontal axis.
		/// </summary>
		/// <param name="dc">Drawing Context via which to draw the axis</param>
		public void Draw(DrawingContext dc)
		{
            if (_axis == null)
            {
                return;
            }

            Pen pen = new Pen(_stroke, _strokeThickness);

			double multiplier = 1;
			if (_tickOrientation == TickOrientation.Up)
			{
				multiplier = -1;
			}

            List<AxisMarking> ticks = _axis.GetAxisMarkings(_physicalMinX, _physicalMaxX);

            int labelCount = 0;
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

				double physical = _axis.WorldToPhysical(tick.World, _physicalMinX, _physicalMaxX, ClippingType.NoClip);
                // make sure that all lines are on integer boundaries, to ensure sharp lines (gridlines set to 0.5, line width 1).
				dc.DrawLine(pen, 
					new Point( (int)physical, (int)_physicalY), 
					new Point( (int)physical, (int)(_physicalY + offset)));

                if (_visibility_textMarkings == Visibility.Visible)
                {
                    if (tick.Text != null)
                    {
                        FormattedText ft = new FormattedText(
                            tick.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                            new Typeface("Arial"), 12.0, Brushes.Black);

                        double yPos = _physicalY + 2;
                        if (_tickOrientation == TickOrientation.Down)
                        {
                            yPos = _physicalY - ft.Height - 2;
                        }

                        if (labelCount++ % 2 == 1 && _alternateHeights)
                        {
                            yPos += ft.Height + 2;
                        }
                        dc.DrawText(ft, new Point(physical - ft.Width / 2.0, yPos));
                    }
                }
			}

            if (Label != null)
            {
                FormattedText ft = new FormattedText(
                    Label, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 12.0, Brushes.Black);

                double xPos = (_physicalMinX + _physicalMaxX) / 2.0 - ft.Width / 2.0;
                double yPos = _physicalY + LabelExtraOffset;
                if (_tickOrientation == TickOrientation.Down)
                {
                    yPos = _physicalY - LabelExtraOffset - ft.Height;
                }

                dc.DrawText(ft, new Point(xPos, yPos));
            }

            // make sure that all lines are on integer boundaries, to ensure sharp lines (gridlines set to 0.5, line width 1).
			dc.DrawLine(pen, new Point((int)_physicalMinX, (int)_physicalY), new Point((int)_physicalMaxX, (int)_physicalY));
		}

        /// <summary>
        /// Minimum physical x of the axis.
        /// </summary>
        public double PhysicalMinX
        {
            get
            {
                return _physicalMinX;
            }
            set
            {
                _physicalMinX = value;
            }
        }
        private double _physicalMinX;

        /// <summary>
        /// Maximum physical x of the axis.
        /// </summary>
        public double PhysicalMaxX
        {
            get
            {
                return _physicalMaxX;
            }
            set
            {
                _physicalMaxX = value;
            }
        }
        private double _physicalMaxX;

        /// <summary>
        /// Physical Y of the axis.
        /// </summary>
        public double PhysicalY
        {
            get
            {
                return _physicalY;
            }
            set
            {
                _physicalY = value;
            }
        }
        private double _physicalY;

        /// <summary>
        /// Gets all axis markings. 
        /// </summary>
        /// <returns>axis markings for the logical axis wrapped by this physical axis.</returns>
        public override List<AxisMarking> GetAxisMarkings()
        {
            return _axis.GetAxisMarkings(_physicalMinX, _physicalMaxX);
        }

        /// <summary>
        /// Returns the physical coordinate corresponding to the provided world coordinate.
        /// </summary>
        /// <param name="world">The world value to transform to physical coordinate</param>
        /// <param name="clippingType">Whether or not to clip.</param>
        /// <returns>physical coordinate corresponding to world provided.</returns>
        public override double WorldToPhysical(double world, ClippingType clippingType)
        {
            return _axis.WorldToPhysical(world, _physicalMinX, _physicalMaxX, clippingType);
        }

        /// <summary>
        /// Returns the world coordinate corresponding to the provided physical coordinate.
        /// </summary>
        /// <param name="physical">the physical coordinate to get world value for</param>
        /// <param name="clippingType">whether or not to clip.</param>
        /// <returns>world coordinate corresponding to physical provided</returns>
        public override double PhysicalToWorld(double physical, ClippingType clippingType)
        {
            return _axis.PhysicalToWorld(physical, _physicalMinX, _physicalMaxX, clippingType);
        }

        public bool AlternateHeights
        {
            get
            {
                return _alternateHeights;
            }
            set
            {
                _alternateHeights = value;
            }
        }
	    private bool _alternateHeights;
	}
}
