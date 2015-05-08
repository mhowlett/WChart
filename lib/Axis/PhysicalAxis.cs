
// (c) 2007 Matthew Howlett

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WChart
{

    /// <summary>
    /// PhysicalAxis wraps an axis and provides functionality related to drawing. This 
    /// class is overriden by physical axes of specific orientation.
    /// </summary>
	public abstract class PhysicalAxis
	{

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="axis">The logical axis wrapped by this class.</param>
        public PhysicalAxis(Axis axis)
        {
            _axis = axis;
        }

        /// <summary>
        /// Length of the large ticks.
        /// </summary>
		public double LargeTickLength
		{
			get
			{
				return _largeTickLength;
			}
            set
            {
                _largeTickLength = value;
            }
		}
		private double  _largeTickLength = 7.0;


        /// <summary>
        /// Length of the small ticks.
        /// </summary>
		public double SmallTickLength
		{
			get
			{
				return _smallTickLength;
			}
            set
            {
                _smallTickLength = value;
            }
		}
		private double _smallTickLength = 4.0;


        /// <summary>
        /// The Axis Label
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
        private string _label;


        /// <summary>
        /// The offset of the label in device pixel units from the axis. The label
        /// is always drawn on the same side of the axis as the tick text.
        /// </summary>
        public double LabelExtraOffset
        {
            get
            {
                return _labelExtraOffset;
            }
            set
            {
                _labelExtraOffset = value;
            }
        }
        private double _labelExtraOffset = 20;


        /// <summary>
        /// Whether or not to show axis text markings.
        /// </summary>
        public System.Windows.Visibility Visibility_TextMarkings
        {
            get
            {
                return _visibility_textMarkings;
            }
            set
            {
                _visibility_textMarkings = value;
            }
        }
        protected Visibility _visibility_textMarkings = Visibility.Visible;


        /// <summary>
        /// The axis wrapped by this class.
        /// </summary>
        public Axis Axis
        {
            get
            {
                return _axis;
            }
            set
            {
                _axis = value;
            }
        }
        protected Axis _axis;


        /// <summary>
        /// Brush used to draw the axis.
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
        protected Brush _stroke = Brushes.Black;


        /// <summary>
        /// Thickness of the brush used to draw the axis.
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
        protected double _strokeThickness = 0.8;



        /// <summary>
        /// Gets all axis markings. 
        /// </summary>
        /// <returns>axis markings for the logical axis wrapped by this physical axis.</returns>
        public abstract List<AxisMarking> GetAxisMarkings();


        /// <summary>
        /// Returns the physical coordinate corresponding to the provided world coordinate.
        /// </summary>
        /// <param name="world">The world value to transform to physical coordinate</param>
        /// <param name="clippingType">Whether or not to clip.</param>
        /// <returns>physical coordinate corresponding to world provided.</returns>
        public abstract double WorldToPhysical(double world, ClippingType clippingType);


        /// <summary>
        /// Returns the world coordinate corresponding to the provided physical coordinate.
        /// </summary>
        /// <param name="physical">the physical coordinate to get world value for</param>
        /// <param name="clippingType">whether or not to clip.</param>
        /// <returns>world coordinate corresponding to physical provided</returns>
        public abstract double PhysicalToWorld(double physical, ClippingType clippingType);

	}
}
