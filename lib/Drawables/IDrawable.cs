
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
    /// An object that can be plotted against a horizontal and vertical axis.
    /// </summary>
    public interface IDrawable
    {

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="dc">DrawingContext to use to draw.</param>
        /// <param name="hAxis">The horizontal axis to draw against.</param>
        /// <param name="vAxis">The vertical axis to draw against.</param>
        void Draw(DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis);

    }
}
