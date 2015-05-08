
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
    /// An item in legend with a filled box symbol.
    /// </summary>
    public class LegendItem_FilledBox : LegendItem
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">the legend item label</param>
        public LegendItem_FilledBox( string label, Brush fill )
            : base(label)
        {
            _fill = fill;
        }


        /// <summary>
        /// The brush used to fill the box.
        /// </summary>
        public Brush Fill
        {
            get
            {
                return _fill;
            }
        }
        private Brush _fill;


        /// <summary>
        /// Dimensions of the legend item.
        /// </summary>
        public override System.Windows.Rect Dimensions
        {
            get 
            {
                return new Rect(0, 0, _formattedText.Width + 40, _formattedText.Height + 4);
            }
        }


        /// <summary>
        /// Draw the legend item
        /// </summary>
        /// <param name="dc">DrawingContext on which to draw</param>
        /// <param name="position">The position on the drawing context to draw relative to.</param>
        public override void Draw(System.Windows.Media.DrawingContext dc, System.Windows.Point position)
        {
            Pen linePen = new Pen(Brushes.Black, 1.0);
            dc.DrawRectangle(_fill, linePen, new Rect( (int)position.X + 2, (int)position.Y+2, 28, (int)Dimensions.Height-4 ) );
            dc.DrawText(_formattedText, new Point(38 + position.X, 2 + position.Y));
        }

    }
}
