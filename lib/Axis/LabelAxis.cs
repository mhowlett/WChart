
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Text;

namespace WChart
{
	
    /// <summary>
    /// Label axis - an axis with user specified labels.
    /// </summary>
    public class LabelAxis : Axis
	{

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="worldMin">The world value corresponding to the minimum position of the axis.</param>
		/// <param name="worldMax">The world value corresponding to the maximum position of the axis.</param>
		public LabelAxis(double worldMin, double worldMax)
		    : base( worldMin, worldMax )
        {
		}


        /// <summary>
		/// Gets a list of markings to display on this axis
		/// </summary>
		/// <param name="physicalMin">The physical minimum extent of the axis.</param>
		/// <param name="physicalMax">The physical maximum extent of the axis.</param>
		/// <returns>list of markings for the axis.</returns>
        public override List<AxisMarking> GetAxisMarkings(double physicalMin, double physicalMax)
        {
            List<AxisMarking> markings = new List<AxisMarking>();
            for (int i = 0; i < _tickLabels.Count; ++i)
            {
                markings.Add( new AxisMarking(_tickPositions[i], TickType.Large, _tickLabels[i]) );
            }
            return markings;
        }


        /// <summary>
        /// Adds a label to be displayed. A large tick will be drawn with the label.
        /// </summary>
        /// <param name="worldLocation">the world location of the tick / label</param>
        /// <param name="label">label to add</param>
        public void AddLabel(double worldLocation, string label)
        {
            _tickLabels.Add(label);
            _tickPositions.Add(worldLocation);
        }


        /// <summary>
        /// label/tick positions added.
        /// </summary>
        private List<double> _tickPositions = new List<double>();


        /// <summary>
        /// labels added. 
        /// </summary>
        private List<string> _tickLabels = new List<string>();
	}

}
