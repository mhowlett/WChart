
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Text;

namespace WChart
{
    
    /// <summary>
    /// An axis with one label per day corresponding to the day of the month number. The
    /// labels are placed at the start of the day. Ticks are placed 1/2 way between labels.
    /// </summary>
    public class DayAxis : Axis
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="worldMin">World minimum (DateTime ticks)</param>
        /// <param name="worldMax">World maximum (DateTime ticks)</param>
        public DayAxis(double worldMin, double worldMax)
            : base(worldMin, worldMax)
        {
        }


        /// <summary>
        /// returns the set of ticks and markings for the axis.
        /// </summary>
        /// <param name="physicalMin">minimum physical extent of the axis.</param>
        /// <param name="physicalMax">maximum physical extent of the axis.</param>
        /// <returns>list of markings for the axis.</returns>
        public override List<AxisMarking> GetAxisMarkings(double physicalMin, double physicalMax)
        {
            DateTime start = new DateTime((long)WorldMin);
            DateTime end = new DateTime((long)WorldMax);

            if (end - start > new TimeSpan(65, 0, 0, 0, 0))
            {
                throw new WChartException("too many days to put on day axis");
            }

            List<AxisMarking> ticks = new List<AxisMarking>();
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
            DateTime startDay = new DateTime((start.Ticks / oneDay.Ticks)*oneDay.Ticks); // discard remainder.
            DateTime current = startDay;
            while (true)
            {
                if (current < start) // could happen with first one.
                {
                    current += oneDay;
                    continue; 
                }

                ticks.Add(new AxisMarking(current.Ticks, TickType.None, current.Day.ToString()));

                long tickPlace = current.Ticks + oneDay.Ticks / 2;
                if (tickPlace < WorldMax)
                {
                    ticks.Add(new AxisMarking(tickPlace, TickType.Small, null));
                }

                current += oneDay;

                if (current > end)
                {
                    break;
                }
            }

            return ticks;
        }

    }

}
