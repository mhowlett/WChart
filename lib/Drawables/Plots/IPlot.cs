
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
    /// Encapsulates an object that can be drawn against a pair of horizontal and vertical axes.
    /// </summary>
	public interface IPlot : IDrawable
	{

        /// <summary>
        /// A suitable X axis to chart against. Or null, if this plot should have no impact on axes.
        /// </summary>
        /// <returns>A suitable X axis to chart against. Or null, if this plot should have no impact on axes.</returns>
		Axis SuggestedAxisX();
		

        /// <summary>
        /// A suitable Y axis to chart against. Or null, if this plot should have no impact on axes.
        /// </summary>
        /// <returns>A suitable Y axis to chart against. Or null, if this plot should have no impact on axes.</returns>
		Axis SuggestedAxisY();


        /// <summary>
        /// Adds a representation of this plot to a legend.
        /// </summary>
        /// <param name="legend">The legend to add a representation of this plot to.</param>
        void AddToLegend(Legend legend);
	}

}
