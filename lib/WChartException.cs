using System;

namespace WChart
{
    /// <summary>
    ///     Basic exception type for WPlot errors.
    /// </summary>
	public class WChartException : Exception
	{
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="message">
        ///     message describing the error.
        /// </param>
		public WChartException(string message)
			: base(message)
		{
		}
	}
}
