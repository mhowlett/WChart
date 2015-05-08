
// (c) 2007 Matthew Howlett

using System;
using System.Collections.Generic;
using System.Text;

namespace WChart
{
    /*
	public class MarketDataAxis : Axis
	{
		public override double WorldToPhysical(double world, double physicalMin, double physicalMax, ClippingType clippingType)
		{
			return 40;
			throw new Exception("The method or operation is not implemented.");
		}

		public MarketDataAxis( MarketData md )
		{
			_md = md;
		}

		private MarketData _md; 

		public override List<Tick> GetTicks(double physicalMin, double physicalMax)
		{

			List<Tick> ticks = new List<Tick>();
			List<DateTime> times = new List<DateTime>();

			for (int i = 0; i < _md.Times.Count - 1; ++i)
			{
				if (_md.Times[i + 1].Month != _md.Times[i].Month)
				{
					double prop = (double)i / (_md.Times.Count);
					double pl = prop * (physicalMax - physicalMin) + physicalMin;
					Tick tick = new Tick(0.0, TickType.Large, null);
					ticks.Add(tick);
					times.Add( _md.Times[i+1] );
				}
			}
			
			// now we know how many ticks, can compensate for name sizes.

			for (int i = 0; i < ticks.Count; ++i)
			{
				switch (times[i].Month)
				{
					case 1:
						ticks[i].Text = times[i].Year.ToString().Substring(2);
						break;
					case 2:
						ticks[i].Text = "Feb";
						break;
					case 3:
						ticks[i].Text = "Mar";
						break;
					case 4:
						ticks[i].Text = "Apr";
						break;
					case 5:
						ticks[i].Text = "May";
						break;
					case 6:
						ticks[i].Text = "Jun";
						break;
					case 7:
						ticks[i].Text = "Jul";
						break;
					case 8:
						ticks[i].Text = "Aug";
						break;
					case 9:
						ticks[i].Text = "Sep";
						break;
					case 10:
						ticks[i].Text = "Oct";
						break;
					case 11:
						ticks[i].Text = "Nov";
						break;
					case 12:
						ticks[i].Text = "Dec";
						break;
					default:
						throw new Exception("unknown month");
				}
				
			}

			return ticks;
		}
	}
*/
}
