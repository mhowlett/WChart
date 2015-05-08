using System.Collections.Generic;

namespace WChart
{
    /// <summary>
    ///     Utility functions specific to WPlot.
    /// </summary>
	public static class Utils
	{
        /// <summary>
        ///     A small number. 
        /// </summary>
		public static double Epsilon
		{
			get
			{
				return 0.00000001;
			}
		}

        /// <summary>
        ///     Converts a list of values into a list of these values progressively accumulated.
        /// </summary>
        /// <param name="vs">
        ///     the list to accumulate
        /// </param>
        /// <returns>
        ///     vs accumulated
        /// </returns>
        public static List<double> AccumulateValues(List<double> vs)
        {
            List<double> result = new List<double>();
            double sum = 0.0;
            for (int i = 0; i < vs.Count; ++i)
            {
                sum += vs[i];
                result.Add(sum);
            }
            return result;
        }
    }
}
