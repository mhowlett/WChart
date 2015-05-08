
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;

namespace WChart
{

    /// <summary>
    /// A DateTime axis
    /// </summary>
    public class DateTimeAxis : Axis
    {
        
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="worldMin">The world value corresponding to the minimum position of the axis.</param>
		/// <param name="worldMax">The world value corresponding to the maximum position of the axis.</param>
		public DateTimeAxis(double worldMin, double worldMax)
            : base(worldMin, worldMax)
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="worldMin">The world value corresponding to the minimum position of the axis.</param>
		/// <param name="worldMax">The world value corresponding to the maximum position of the axis.</param>
		public DateTimeAxis(DateTime worldMin, DateTime worldMax)
			: base(worldMin.Ticks, worldMax.Ticks)
		{
		}


        /// <summary>
        /// Gets markings for the axis.
        /// </summary>
        /// <param name="physicalMin">minimum physical extent of the axis.</param>
        /// <param name="physicalMax">maximum physical extent of the axis.</param>
        /// <returns>Set of markings for the axis.</returns>
        public override List<AxisMarking> GetAxisMarkings(double physicalMin, double physicalMax)
        {
            List<AxisMarking> ticks = new List<AxisMarking>();

            TimeSpan axisTimeLength = new TimeSpan((long)WorldRange);
            DateTime worldMinDate = new DateTime((long)WorldMin);
            DateTime worldMaxDate = new DateTime((long)WorldMax);

            const int daysInMonth = 30;


            // if less than 2 minutes, then large ticks on second spacings. 

            if (axisTimeLength < new TimeSpan(0, 0, 2, 0, 0))
            {
                double secondsSkip;

                if (axisTimeLength < new TimeSpan(0, 0, 0, 10, 0))
                    secondsSkip = 1.0;
                else if (axisTimeLength < new TimeSpan(0, 0, 0, 20, 0))
                    secondsSkip = 2.0;
                else if (axisTimeLength < new TimeSpan(0, 0, 0, 50, 0))
                    secondsSkip = 5.0;
                else if (axisTimeLength < new TimeSpan(0, 0, 2, 30, 0))
                    secondsSkip = 15.0;
                else
                    secondsSkip = 30.0;

                int second = worldMinDate.Second;
                second -= second % (int)secondsSkip;

                DateTime currentTickDate = new DateTime(
                    worldMinDate.Year,
                    worldMinDate.Month,
                    worldMinDate.Day,
                    worldMinDate.Hour,
                    second, 0, 0);

                while (currentTickDate <= worldMaxDate)
                {
                    double world = (double)currentTickDate.Ticks;

                    if (world >= WorldMin && world <= WorldMax)
                    {

                        string minutes = currentTickDate.Minute.ToString();
                        string seconds = currentTickDate.Second.ToString();
                        if (seconds.Length == 1)
                        {
                            seconds = "0" + seconds;
                        }

                        if (minutes.Length == 1)
                        {
                            minutes = "0" + minutes;
                        }
                        string label = currentTickDate.Hour + ":" + minutes + "." + seconds;

                        ticks.Add(new AxisMarking(world, TickType.Large, label));
                    }

                    currentTickDate = currentTickDate.AddSeconds(secondsSkip);
                }

                return ticks;
            }


            // Less than 2 hours, then large ticks on minute spacings.

            else if (axisTimeLength < new TimeSpan(0, 2, 0, 0, 0))
            {

                double minuteSkip;

                if (axisTimeLength < new TimeSpan(0, 0, 10, 0, 0))
                    minuteSkip = 1.0;
                else if (axisTimeLength < new TimeSpan(0, 0, 20, 0, 0))
                    minuteSkip = 2.0;
                else if (axisTimeLength < new TimeSpan(0, 0, 50, 0, 0))
                    minuteSkip = 5.0;
                else if (axisTimeLength < new TimeSpan(0, 2, 30, 0, 0))
                    minuteSkip = 15.0;
                else //( timeLength < new TimeSpan( 0,5,0,0,0) )
                    minuteSkip = 30.0;

                int minute = worldMinDate.Minute;
                minute -= minute % (int)minuteSkip;

                DateTime currentTickDate = new DateTime(
                    worldMinDate.Year,
                    worldMinDate.Month,
                    worldMinDate.Day,
                    worldMinDate.Hour,
                    minute, 0, 0);

                while (currentTickDate <= worldMaxDate)
                {
                    double world = (double)currentTickDate.Ticks;

                    if (world >= this.WorldMin && world <= this.WorldMax)
                    {
                        string minutes = currentTickDate.Minute.ToString();
                        if (minutes.Length == 1)
                        {
                            minutes = "0" + minutes;
                        }
                        string label = currentTickDate.Hour.ToString() + ":" + minutes;

                        ticks.Add(new AxisMarking(world, TickType.Large, label));
                    }

                    currentTickDate = currentTickDate.AddMinutes(minuteSkip);
                }

                return ticks;
            }


            // Less than 2 days, then large ticks on hour spacings.

            else if (axisTimeLength < new TimeSpan(2, 0, 0, 0, 0))
            {

                double hourSkip;
                if (axisTimeLength < new TimeSpan(0, 10, 0, 0, 0))
                    hourSkip = 1.0;
                else if (axisTimeLength < new TimeSpan(0, 20, 0, 0, 0))
                    hourSkip = 2.0;
                else
                    hourSkip = 6.0;


                int hour = worldMinDate.Hour;
                hour -= hour % (int)hourSkip;

                DateTime currentTickDate = new DateTime(
                    worldMinDate.Year,
                    worldMinDate.Month,
                    worldMinDate.Day,
                    hour, 0, 0, 0);

                while (currentTickDate <= worldMaxDate)
                {
                    double world = (double)currentTickDate.Ticks;

                    if (world >= this.WorldMin && world <= this.WorldMax)
                    {
                        string minutes = currentTickDate.Minute.ToString();
                        if (minutes.Length == 1)
                        {
                            minutes = "0" + minutes;
                        }
                        string label = currentTickDate.Hour.ToString() + ":" + minutes;

                        ticks.Add(new AxisMarking(world, TickType.Large, label));
                    }

                    currentTickDate = currentTickDate.AddHours(hourSkip);
                }

                return ticks;
            }


            // less than 5 months, then large ticks on day spacings.

            else if (axisTimeLength < new TimeSpan(daysInMonth * 4, 0, 0, 0, 0))
            {

                double daySkip;
                if (axisTimeLength < new TimeSpan(10, 0, 0, 0, 0))
                    daySkip = 1.0;
                else if (axisTimeLength < new TimeSpan(20, 0, 0, 0, 0))
                    daySkip = 2.0;
                else if (axisTimeLength < new TimeSpan(7 * 10, 0, 0, 0, 0))
                    daySkip = 7.0;
                else
                    daySkip = 14.0;

                DateTime currentTickDate = new DateTime(
                    worldMinDate.Year,
                    worldMinDate.Month,
                    worldMinDate.Day);

                if (daySkip == 2.0)
                {

                    TimeSpan timeSinceBeginning = currentTickDate - DateTime.MinValue;

                    if (timeSinceBeginning.Days % 2 == 1)
                        currentTickDate = currentTickDate.AddDays(-1.0);
                }

                if (daySkip == 7 || daySkip == 14.0)
                {
                    DayOfWeek dow = currentTickDate.DayOfWeek;
                    switch (dow)
                    {
                        case DayOfWeek.Monday:
                            break;
                        case DayOfWeek.Tuesday:
                            currentTickDate = currentTickDate.AddDays(-1.0);
                            break;
                        case DayOfWeek.Wednesday:
                            currentTickDate = currentTickDate.AddDays(-2.0);
                            break;
                        case DayOfWeek.Thursday:
                            currentTickDate = currentTickDate.AddDays(-3.0);
                            break;
                        case DayOfWeek.Friday:
                            currentTickDate = currentTickDate.AddDays(-4.0);
                            break;
                        case DayOfWeek.Saturday:
                            currentTickDate = currentTickDate.AddDays(-5.0);
                            break;
                        case DayOfWeek.Sunday:
                            currentTickDate = currentTickDate.AddDays(-6.0);
                            break;
                    }

                }

                if (daySkip == 14.0f)
                {
                    TimeSpan timeSinceBeginning = currentTickDate - DateTime.MinValue;

                    if ((timeSinceBeginning.Days / 7) % 2 == 1)
                    {
                        currentTickDate = currentTickDate.AddDays(-7.0);
                    }
                }

                while (currentTickDate <= worldMaxDate)
                {
                    double world = (double)currentTickDate.Ticks;

                    if (world >= this.WorldMin && world <= this.WorldMax)
                    {
                        string label = (currentTickDate.Day).ToString();
                        label += " ";
                        label += currentTickDate.ToString("MMM");

                        ticks.Add(new AxisMarking(world, TickType.Large, label));
                    }

                    currentTickDate = currentTickDate.AddDays(daySkip);
                }

                return ticks;
            }


            // else ticks on month or year spacings.

            /*
            else if (timeLength >= new TimeSpan(daysInMonth * 4, 0, 0, 0, 0))
            {

                int monthSpacing = 0;

                if (timeLength.Days < daysInMonth * (12 * 3 + 6))
                {
                    LargeTickLabelType_ = LargeTickLabelType.month;

                    if (timeLength.Days < daysInMonth * 10)
                        monthSpacing = 1;
                    else if (timeLength.Days < daysInMonth * (12 * 2))
                        monthSpacing = 3;
                    else // if ( timeLength.Days < daysInMonth*(12*3+6) )
                        monthSpacing = 6;
                }
                else
                {
                    LargeTickLabelType_ = LargeTickLabelType.year;

                    if (timeLength.Days < daysInMonth * (12 * 6))
                        monthSpacing = 12;
                    else if (timeLength.Days < daysInMonth * (12 * 12))
                        monthSpacing = 24;
                    else if (timeLength.Days < daysInMonth * (12 * 30))
                        monthSpacing = 60;
                    else
                        monthSpacing = 120;
                    //LargeTickLabelType_ = LargeTickLabelType.none;
                }

                // truncate start
                DateTime currentTickDate = new DateTime(
                    worldMinDate.Year,
                    worldMinDate.Month,
                    1);

                if (monthSpacing > 1)
                {
                    currentTickDate = currentTickDate.AddMonths(
                        -(currentTickDate.Month - 1) % monthSpacing);
                }

                // Align on 2 or 5 year boundaries if necessary.
                if (monthSpacing >= 24)
                {
                    currentTickDate = currentTickDate.AddYears(
                        -(currentTickDate.Year) % (monthSpacing / 12));
                }

                //this.firstLargeTick_ = (double)currentTickDate.Ticks;

                if (LargeTickLabelType_ != LargeTickLabelType.none)
                {
                    while (currentTickDate < worldMaxDate)
                    {
                        double world = (double)currentTickDate.Ticks;

                        if (world >= this.WorldMin && world <= this.WorldMax)
                        {
                            largeTickPositions.Add(world);
                        }

                        currentTickDate = currentTickDate.AddMonths(monthSpacing);
                    }
                }
            }
            */

            return ticks;
        }

    }
}
