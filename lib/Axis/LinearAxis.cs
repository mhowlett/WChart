
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WChart
{

	/// <summary>
	/// Encapsulates functionality for working out tick and axis label placements for a
	/// standard linear axis. The tick placement could be made more user definable, but
	/// the tick placement is carefully chosen and it is unlikely you would want to 
	/// overide the defaults. If you do, does a LabelAxis suit your needs? 
	/// </summary>
	public class LinearAxis : Axis
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="worldMin">The world value corresponding to the minimum position of the axis.</param>
		/// <param name="worldMax">The world value corresponding to the maximum position of the axis.</param>
		public LinearAxis(double worldMin, double worldMax)
		    : base( worldMin, worldMax )
        {
		}


		/// <summary>
		/// Determines the world spacing to use between large ticks. Chooses values such that 
		/// there are between 3 and 6 ticks.
		/// </summary>
		/// <returns>the world spacing to use between large ticks.</returns>
		private void DetermineTickSpacing( out double largeTickSpacing, out int numberSmallTicks, double physicalMin, double physicalMax )
		{
			double log = Math.Log10(WorldRange);
			double fraction = log - Math.Floor(log);

			// the following ensures there are between 3 and 6 large ticks. 
            int multiplier = -1;
			if (fraction == 0.0)
			{
				largeTickSpacing = Math.Pow(10, Math.Floor(log) - 1) * 2.0;
				numberSmallTicks = 1;
                multiplier = 2;
			}
			else if (fraction <= (Math.Log10(25) - Math.Floor(Math.Log10(25))))
			{
				largeTickSpacing = Math.Pow(10, Math.Floor(log) - 1) * 5.0;
				numberSmallTicks = 4;
                multiplier = 5;
			}
			else if (fraction <= (Math.Log10(50) - Math.Floor(Math.Log10(50))))
			{
				largeTickSpacing = Math.Pow(10, Math.Floor(log));
				numberSmallTicks = 0;
                multiplier = 1;
			}
			else
			{
				largeTickSpacing = Math.Pow(10, Math.Floor(log)) * 2.0;
				numberSmallTicks = 1;
                multiplier = 2;
			}

            /*
            // adjust for minimum physical spacing constraint.
            while (true)
            {
                double physicalLength =
                    Math.Abs(WorldToPhysical(largeTickSpacing, physicalMin, physicalMax, ClippingType.NoClip) -
                              WorldToPhysical(0.0, physicalMin, physicalMax, ClippingType.NoClip));

                if (physicalLength >= MinPhysicalTickSeparation)
                {
                    break;
                }

                switch (multiplier)
                {
                    case 1:
                        multiplier = 2;
                        largeTickSpacing *= 2;
                        numberSmallTicks = 1;
                        break;
                    case 2:
                        multiplier = 5;
                        largeTickSpacing *= 5.0 / 2.0;
                        numberSmallTicks = 5;
                        break;
                    case 5:
                        multiplier = 1;
                        largeTickSpacing *= 2;
                        numberSmallTicks = 1;
                        break;
                    default:
                        throw new WPlotException("unexpected multiplier.");
                }
            }
             */

		}


		/// <summary>
		/// Given the spacing, determines the first position in the axis range such that
		/// position mod spacing is zero.
		/// </summary>
		/// <param name="spacing">the spacing of ticks</param>
		/// <returns>first large tick position in the axis range.</returns>
		private double DetermineFirstLargeTickPosition(double spacing)
		{
			double fraction = WorldMin / spacing - Math.Floor(WorldMin / spacing);
			
			if (Math.Abs(fraction) < Utils.Epsilon / spacing || Math.Abs(fraction - spacing) < Utils.Epsilon / spacing)
			{
				return WorldMin;
			}
			else
			{
				return (Math.Floor(WorldMin / spacing) + 1) * spacing;
			}
		}


        private List<AxisMarking> GetLimitedAxisMarkings( double largeTickSpacing )
        {
            double pos = DetermineFirstLargeTickPosition(largeTickSpacing);
            List<double> tickPositions = new List<double>();

            int cnt = 0;
            while (pos < WorldMax + Utils.Epsilon && cnt < 100)
            {
                tickPositions.Add(pos);
                pos += largeTickSpacing;
            }

            System.Diagnostics.Debug.Assert(tickPositions.Count > 1);

            List<AxisMarking> ticks = new List<AxisMarking>();
            ticks.Add(new AxisMarking(tickPositions[0], TickType.Large, String.Format("{0:" + _tickLabelFormat + "}", tickPositions[0])));
            for (int i = 1; i < tickPositions.Count - 1; ++i)
            {
                ticks.Add( new AxisMarking(tickPositions[i], TickType.Small, null ) );
            }
            ticks.Add(new AxisMarking(tickPositions[tickPositions.Count - 1], TickType.Large, String.Format("{0:" + _tickLabelFormat + "}", tickPositions[tickPositions.Count - 1])));

            return ticks;
        }


        /// <summary>
        /// Gets a list of ticks to display on this axis
        /// </summary>
        /// <param name="physicalMin">The physical minimum extent of the axis.</param>
        /// <param name="physicalMax">The physical maximum extend of the axis.</param>
        /// <returns>list of axis markings.</returns>
        public override List<AxisMarking> GetAxisMarkings(double physicalMin, double physicalMax)
		{
			double spacing;
			int numberSmallTicks;

			DetermineTickSpacing( out spacing, out numberSmallTicks, physicalMin, physicalMax );

            // if the large ticks aren't very far appart, return a limited set of markings only.
            double physicalLargeTickSpacing =
                Math.Abs(WorldToPhysical(spacing, physicalMin, physicalMax, ClippingType.NoClip) -
                         WorldToPhysical(0.0, physicalMin, physicalMax, ClippingType.NoClip));

            if (physicalLargeTickSpacing < MinPhysicalTickSeparation)
            {
                return GetLimitedAxisMarkings(spacing);
            }

            double pos = DetermineFirstLargeTickPosition(spacing);
            double smallTickSpacing = spacing / (double)(numberSmallTicks +1);

			List<AxisMarking> ticks = new List<AxisMarking>();

			// create first lot of small ticks before first large tick.
			double backLocation = pos;
			for (int i = 0; i < numberSmallTicks; ++i)
			{
				backLocation -= smallTickSpacing;
				if (backLocation < WorldMin - Utils.Epsilon)
				{
					break;
				}
				ticks.Add(new AxisMarking(backLocation, TickType.Small, null));
			}


			// create ticks after first large tick.
            int largeTickCount = 0;
            int cnt = 0;
			while (pos < WorldMax + Utils.Epsilon && cnt < 100)
			{
				ticks.Add( new AxisMarking(pos, TickType.Large, String.Format( "{0:" + _tickLabelFormat + "}", pos )));
                largeTickCount += 1;
				double smallPos = pos;
				for (int i = 0; i < numberSmallTicks; ++i)
				{
					smallPos += smallTickSpacing;
					if (smallPos > WorldMax + Utils.Epsilon)
					{
						break;
					}
					ticks.Add(new AxisMarking(smallPos, TickType.Small, null));
				}

				pos += spacing;
                cnt += 1;
			}

            return ticks;

		}

        public string TickLabelFormat
        {
            get
            {
                return _tickLabelFormat;
            }
            set
            {
                _tickLabelFormat = value;
            }
        }
        private string _tickLabelFormat = "0.0";

	}

}
