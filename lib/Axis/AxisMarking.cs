
// (c) 2007 Matthew Howlett

namespace WChart
{

	/// <summary>
	/// Describes a marking placed on an axis. A marking consists of text and/or tick.
	/// </summary>
	public class AxisMarking
	{
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="world">the world location of the marking</param>
		/// <param name="type">the tick type</param>
		/// <param name="text">text associated with the marking (can be null)</param>
		public AxisMarking(double world, TickType type, string text)
		{
			_world = world;
			_tickType = type;
			Text = text;
		}


		/// <summary>
		/// The world coordinate of the marking.
		/// </summary>
		public double World
		{
			get
			{
				return _world;
			}
		}
		private readonly double _world;


		/// <summary>
		/// The text associated with the marking
		/// </summary>
		public string Text { get; set; }


		/// <summary>
		/// The type of tick
		/// </summary>
		public TickType TickType
		{
			get
			{
				return _tickType;
			}
		}
		private readonly TickType _tickType;

	}

}
