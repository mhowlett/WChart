
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
    /// Provides functionality for drawing legends.
    /// </summary>
    public class Legend
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="desiredBounds">The desired bounds of legend.</param>
        /// <param name="extendDirection">If there are two many items, extend in this direction.</param>
        /// <param name="shrinkToFit">If the desired bounds rectangle is too big and this is true, the bounds will shrink as necessary.</param>
        public Legend( Rect desiredBounds, LegendExtendDirection extendDirection, bool shrinkToFit )
        {
            _desiredBounds = desiredBounds;
            _extendDirection = extendDirection;
            _shrinkToFit = shrinkToFit;
        }


        /// <summary>
        /// Add a legend item to the legend.
        /// </summary>
        /// <param name="legendItem">legend item to add.</param>
        public void AddItem(LegendItem legendItem)
        {
            _legendItems.Add(legendItem);
        }

        
        /// <summary>
        /// list of all legend items to display in the legend.
        /// </summary>
        private List<LegendItem> _legendItems = new List<LegendItem>();


        /// <summary>
        /// Desired bounds of legend.
        /// </summary>
        public Rect DesiredBounds
        {
            get
            {
                return _desiredBounds;
            }
            set
            {
                _desiredBounds = value;
            }
        }
        private Rect _desiredBounds;


        /// <summary>
        /// If the items can't fit in the desired bounds, extend in this direction.
        /// </summary>
        public LegendExtendDirection ExtendDirection
        {
            get
            {
                return _extendDirection;
            }
            set
            {
                _extendDirection = value;
            }
        }
        private LegendExtendDirection _extendDirection;

        
        /// <summary>
        /// Draws the legend.
        /// </summary>
        /// <param name="dc">Drawing context on which to draw.</param>
        public void Draw(DrawingContext dc)
        {
            Rect intBounds = new Rect((int)_desiredBounds.Left, (int)_desiredBounds.Top, (int)_desiredBounds.Width, (int)_desiredBounds.Height);

            double currentX = 0.0;
            double currentY = 0.0;

            if (ExtendDirection == LegendExtendDirection.Left)
            {
                double itemTopMargin = 5.0;
                double itemLeftMargin = 10.0;

                currentX = 0.0;
                currentY = itemTopMargin;

                double maxHeight = 0.0;
                for (int i = 0; i < _legendItems.Count; ++i)
                {
                    currentX += itemLeftMargin;
                    _legendItems[i].Draw(dc, new Point(currentX + intBounds.Left, currentY + intBounds.Top));
                    currentX += _legendItems[i].Dimensions.Width;
                    if (_legendItems[i].Dimensions.Height > maxHeight)
                    {
                        maxHeight = _legendItems[i].Dimensions.Height;
                    }
                    if (i != _legendItems.Count - 1)
                    {
                        if (currentX + _legendItems[i+1].Dimensions.Width > intBounds.Width)
                        {
                            currentX = 0.0;
                            currentY += maxHeight;
                            maxHeight = 0.0;
                        }
                    }
                }
                currentX += itemLeftMargin;
                currentY += maxHeight;
                currentY += itemTopMargin - 1;
            }
            else 
            {
                throw new WChartException("unsuported extend direction");
            }

            /*
            if (_shrinkToFit)
            {
                if (currentX < intBounds.Width)
                {
                    intBounds = new Rect(intBounds.Left, intBounds.Top, currentX, intBounds.Height);
                }
                if (currentY < intBounds.Height)
                {
                    intBounds = new Rect(intBounds.Left, intBounds.Top, intBounds.Width, currentY);
                }
            }
          
            dc.DrawRoundedRectangle(Brushes.Transparent, new Pen(_stroke, _strokeThickness), intBounds, 3.0, 3.0);
            */
        }


        /// <summary>
        /// Brush to use to draw the line.
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return _stroke;
            }
            set
            {
                _stroke = value;
            }
        }
        private Brush _stroke = Brushes.Gray;


        /// <summary>
        /// Thickness of the line.
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
            set
            {
                _strokeThickness = value;
            }
        }
        private double _strokeThickness = 1.0;


        /// <summary>
        /// If the desired bounds rectangle is too big and this is true, the bounds will shrink as necessary.
        /// </summary>
        public bool ShrinkToFit
        {
            get
            {
                return _shrinkToFit;
            }
        }
        private bool _shrinkToFit;

    }

}
