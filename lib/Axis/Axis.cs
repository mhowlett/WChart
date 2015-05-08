using System.Collections.Generic;

namespace WChart
{
    /// <summary>
    ///     Provides core functionality for logical axes.
    /// </summary>
	public abstract class Axis
	{
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="worldMin">
        ///     The world value corresponding to the minimum position of the axis.
        /// </param>
        /// <param name="worldMax">
        ///     The world value corresponding to the maximum position of the axis.
        /// </param>
        protected Axis(double worldMin, double worldMax)
        {
            this.worldMin = worldMin;
            this.worldMax = worldMax;
        }

        /// <summary>
        /// Determines the least upper bound of this axis and another one, "a".
        /// </summary>
        /// <param name="a">The other axis to use in LUB calculation.</param>
        public void SetToLUB(Axis a)
        {
            if (a == null)
            {
                throw new WChartException("SetToLUB must be called with an axis instance");
            }

            if (a.WorldMax > WorldMax)
            {
                worldMax = a.WorldMax;
            }

            if (a.WorldMin < WorldMin)
            {
                worldMin = a.WorldMin;
            }
        }

        /// <summary>
        ///     A list of markings (text and ticks) for the axis. The main point of extension of this class.
        ///     Note: The physical extents are usefull in determining h
        /// </summary>
        /// <param name="physicalMin">
        ///     The physical minimum extent of the axis.
        /// </param>
        /// <param name="physicalMax">
        ///     The physical maximum extent of the axis.
        /// </param>
        /// <returns>
        ///     List of axis markings.
        /// </returns>
        public abstract List<AxisMarking> GetAxisMarkings(double physicalMin, double physicalMax);

        /// <summary>
        ///     Transforms a physical value to the corresponding world value.
        /// </summary>
        /// <param name="physical">
        ///     The physical value to get world value for
        /// </param>
        /// <param name="physicalMin">
        ///     The minimum physical extent of the axis
        /// </param>
        /// <param name="physicalMax">
        ///     The maximum physical extent of the axis
        /// </param>
        /// <param name="clippingType">
        ///     Whether or not to snap to the max / min world value if outside this range
        /// </param>
        /// <returns>
        ///     the world value corresponding to the supplied physical value
        /// </returns>
        public virtual double PhysicalToWorld(double physical, double physicalMin, double physicalMax, ClippingType clippingType)
        {
            double world = ((physical - physicalMin) / (physicalMax - physicalMin)) * WorldRange + WorldMin;
            if (clippingType == ClippingType.Clip)
            {
                if (world > WorldMax)
                {
                    world = WorldMax;
                }
                else if (world < WorldMin)
                {
                    world = WorldMin;
                }
            }
            return world;
        }

        /// <summary>
        ///     Transforms a world to a physical coordinate transform. The default transform is linear,
        ///     subclasses may change this behavior. 
        /// </summary>
        /// <param name="clippingType">
        ///     Whether or not to clip the transform to be within the bounds of the physical axis extent
        /// </param>
        /// <param name="world">
        ///     The world coordinate to transform
        /// </param>
        /// <param name="physicalMin">
        ///     The minimum physical coordinate
        /// </param>
        /// <param name="physicalMax">
        ///     The maximum physical coordinate
        /// </param>
        /// <returns>
        ///     The physical coordinate corresponding to the world coordinate.
        /// </returns>
        public virtual double WorldToPhysical(double world, double physicalMin, double physicalMax, ClippingType clippingType)
        {
            double physical = ((world - WorldMin) / WorldRange) * (physicalMax - physicalMin) + physicalMin;
            if (clippingType == ClippingType.NoClip)
            {
                return physical;
            }
            if (physicalMax > physicalMin)
            {
                if (physical < physicalMin)
                {
                    return physicalMin;
                }
                if (physical > physicalMax)
                {
                    return physicalMax;
                }
            }
            else
            {
                if (physical > physicalMin)
                {
                    return physicalMin;
                }
                if (physical < physicalMax)
                {
                    return physicalMax;
                }
            }
            return physical;
        }

        /// <summary>
        ///     The world value corresponding to the minimum position of the axis.
        /// </summary>
        public double WorldMin
        {
            get
            {
                return worldMin;
            }
            set
            {
                worldMin = value;
            }
        }
        private double worldMin;

        /// <summary>
        ///     The world value corresponding to the maximum position of the axis.
        /// </summary>
        public double WorldMax
        {
            get
            {
                return worldMax;
            }
            set
            {
                worldMax = value;
            }
        }
        private double worldMax;

        /// <summary>
        ///     The length of the axis in world coordinates.
        /// </summary>
        public double WorldRange
        {
            get
            {
                return worldMax - worldMin;
            }
        }

		/// <summary>
		///     The minimum physical separation between ticks.
		/// </summary>
		/// <remarks>
		///     Axes don't really use this well yet.
		/// </remarks>
		public double MinPhysicalTickSeparation
		{
			get
			{
				return minPhysicalTickSeparation;
			}
			set
			{
				minPhysicalTickSeparation = value;
			}
		}
		private double minPhysicalTickSeparation = 30.0;
	}
}
