
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
    /// Base class of items that can be drawn in a legend.
    /// </summary>
    public abstract class LegendItem
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">the legend item label</param>
        public LegendItem(string label)
        {
            _label = label;
            _formattedText = new FormattedText(
                label,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                12, Brushes.Black);
        }


        /// <summary>
        /// Legend item label.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }
        private string _label;
        protected FormattedText _formattedText;


        /// <summary>
        /// The dimensions of the item
        /// </summary>
        public abstract Rect Dimensions { get; }

        
        /// <summary>
        /// Draw the item.
        /// </summary>
        /// <param name="dc">The drawing context to use to draw.</param>
        /// <param name="position">The position to draw the item</param>
        public abstract void Draw(DrawingContext dc, Point position);

    }
}
