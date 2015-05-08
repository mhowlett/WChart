
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


namespace WChart.Drawables
{

    /// <summary>
    /// Functionality for drawing text against a horizontal and vertical axis.
    /// </summary>
    public class TextItem : IDrawable
    {

        /// <summary>
        /// Constructor. Text item created will have default typeface Arial, emSize 12 and black brush.
        /// </summary>
        /// <param name="text">text to draw.</param>
        /// <param name="worldPosition">the world coordinates at which to draw the text.</param>
        public TextItem(Point worldPosition, string text)
            : this(worldPosition, text, new Typeface("Arial"), 12.0, Brushes.Black)
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="worldPosition">the world coordinates at which to draw the text.</param>
        /// <param name="text">the text to draw</param>
        /// <param name="typeface">typeface to use to draw the text.</param>
        /// <param name="emSize">size of the text</param>
        /// <param name="brush">brush to use to draw the text</param>
        public TextItem(Point worldPosition, string text, Typeface typeface, double emSize, Brush brush)
        {
            _text = text;
            _typeface = typeface;
            _emSize = emSize;
            _brush = brush;
            _worldPosition = worldPosition;
            _formattedText = new FormattedText(
                _text, 
                System.Globalization.CultureInfo.CurrentCulture, 
                FlowDirection.LeftToRight,
                _typeface, 
                _emSize,
                _brush
            );
        }


        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="dc">DrawingContext to use to draw.</param>
        /// <param name="hAxis">The horizontal axis to draw against.</param>
        /// <param name="vAxis">The vertical axis to draw against.</param>
        public void Draw(System.Windows.Media.DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis)
        {
            Point physicalPosition = new Point(
                hAxis.WorldToPhysical(_worldPosition.X, ClippingType.Clip),
                vAxis.WorldToPhysical(_worldPosition.Y, ClippingType.Clip)
            );

            dc.DrawText(_formattedText, physicalPosition);
        }


        /// <summary>
        /// The text to draw.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
        }
        private string _text;
        private FormattedText _formattedText;


        /// <summary>
        /// The world coordinate at which to draw the text.
        /// </summary>
        public Point WorldPosition
        {
            get
            {
                return _worldPosition;
            }
        }
        private Point _worldPosition;


        /// <summary>
        /// Typeface of the text.
        /// </summary>
        public Typeface Typeface
        {
            get
            {
                return _typeface;
            }
        }
        private Typeface _typeface;


        /// <summary>
        /// Size to draw the text at.
        /// </summary>
        public double EmSize
        {
            get
            {
                return _emSize;
            }
        }
        private double _emSize;


        /// <summary>
        /// Brush to use to draw the text.
        /// </summary>
        public Brush Brush
        {
            get
            {
                return _brush;
            }
        }
        private Brush _brush;

    }

}
