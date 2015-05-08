
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
    /// A item in the legend with line symbol.
    /// </summary>
    public class LegendItem_Line : LegendItem
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">the legend item label</param>
        public LegendItem_Line( string label, Brush stroke, double strokeThickness )
            : base(label)
        {
            _stroke = stroke;
            _strokeThickness = strokeThickness;
        }

        
        /// <summary>
        /// Brush used to draw the line.
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return _stroke;
            }
        }
        private Brush _stroke;


        /// <summary>
        /// Thickness of the line.
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
        }
        private double _strokeThickness;


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
            Pen linePen = new Pen(_stroke,_strokeThickness);
            dc.DrawLine(
                linePen, 
                new Point(position.X + 2, (int)(position.Y + Dimensions.Height / 2.0)), 
                new Point(position.X + 2 + 28, (int)(position.Y + Dimensions.Height / 2.0)));
            dc.DrawText(_formattedText, new Point(38 + position.X, 2 + position.Y));
        }

    }

}
