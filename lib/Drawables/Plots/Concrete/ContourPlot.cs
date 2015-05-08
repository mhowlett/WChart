using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WChart
{

    /// <summary>
    /// Allows plotting a field function as a series of contours.
    /// </summary>
    public class ContourPlot : IPlot
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldMethod">Method which describes the field to be plotted.</param>
        /// <param name="internalGridSize">the grid resolution to work to.</param>
        /// <param name="levels">The contour levels.</param>
        public ContourPlot( FieldDelegate fieldMethod, Dimensions internalGridSize, List<double> levels)
        {
            _fieldMethod = fieldMethod;
            _internalGridSize = internalGridSize;
            _levels = levels;
            _grid = new double[internalGridSize.Rows, internalGridSize.Columns];
        }


        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns>not implemented</returns>
        public Axis SuggestedAxisX()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns>not implemented</returns>
        public Axis SuggestedAxisY()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// not implemented
        /// </summary>
        /// <param name="legend">not implemented</param>
        public void AddToLegend(Legend legend)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Determines the index of the line segment most suitable for having the 
        /// contour label over it. 
        /// </summary>
        /// <param name="lineSegments">the line segments to consider.</param>
        /// <returns>index of line segment that is best place to put label.</returns>
        private int DetermineBestLabelPosition(List<LineSegment> lineSegments, double minX, double maxX, double minY, double maxY)
        {
            double xRange = Math.Abs(maxX - minX);
            double yRange = Math.Abs(maxY - minY);
            double xCenter = (maxX + minX) * 0.5;
            double yCenter = (maxY + minY) * 0.5;

            int minIndex = 0;
            double min = double.MaxValue;
            for (int i = 0; i < lineSegments.Count; ++i)
            {
                
                double dist =
                    (1.0 / Math.Abs(lineSegments[i].X1  - minX)) +
                    (1.0 / Math.Abs(lineSegments[i].X1  - maxX)) + 
                    (1.0 / Math.Abs(lineSegments[i].Y1  - minY)) +
                    (1.0 / Math.Abs(lineSegments[i].Y1  - maxY));

                if (dist < min)
                {
                    min = dist;
                    minIndex = i;
                }
            }

            return minIndex;
        }


        /// <summary>
        /// Draws the contours against a horizontal and vertical axis.
        /// </summary>
        /// <param name="dc">The drawing context on which to draw.</param>
        /// <param name="hAxis">The horizontal physical axis to draw against.</param>
        /// <param name="vAxis">The vertical physical axis to draw against.</param>
        public void Draw(System.Windows.Media.DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis)
        {

            double worldHorizontalRange = hAxis.Axis.WorldMax - hAxis.Axis.WorldMin;
            double worldVerticalRange=  vAxis.Axis.WorldMax - vAxis.Axis.WorldMin;

            for (int i = 0; i < _internalGridSize.Rows; ++i)
            {
                for (int j = 0; j < _internalGridSize.Columns; ++j)
                {
                    double worldY = (double)i / (double)(_internalGridSize.Rows - 1) * worldVerticalRange + vAxis.Axis.WorldMin;
                    double worldX = (double)j / (double)(_internalGridSize.Columns - 1) * worldHorizontalRange + hAxis.Axis.WorldMin;
                    _grid[i, j] = _fieldMethod(worldX, worldY);
                }
            }

            Pair<List<double>, List<List<LineSegment>>> contourGroups = ContourUtils.ContourGenCore.Generate(_grid, _levels);

            double physicalHorizontalRange = hAxis.PhysicalMaxX - hAxis.PhysicalMinX;
            double physicalVerticalRange = vAxis.PhysicalMaxY - vAxis.PhysicalMinY;

            Pen p = new Pen(_stroke, _strokeThickness);
            for (int i = 0; i < contourGroups.First.Count; ++i)
            {
                List<LineSegment> lineSegments = contourGroups.Second[i];

                // find best index to put label. 
                int bestJ = DetermineBestLabelPosition(lineSegments, 0, _internalGridSize.Columns-1, 0, _internalGridSize.Rows-1 );

                // see if line segment sequence is reasonably long. If so, put a contour label on it. 
                bool drawingLabel = false;
                if (_showContourLabels)
                {
                    double sum = 0.0;
                    for (int k = 0; k < lineSegments.Count; ++k)
                    {
                        sum += Math.Abs(lineSegments[k].X1 - lineSegments[k].X2) + Math.Abs(lineSegments[k].Y1 - lineSegments[k].Y2);
                        if (sum > _internalGridSize.Rows / 6.0)
                        {
                            drawingLabel = true;
                            break;
                        }
                    }
                }

                if (drawingLabel)
                {
                    LineSegment ls = lineSegments[bestJ];

                    double x1 = ls.X1 / (double)(_internalGridSize.Columns - 1) * physicalHorizontalRange + hAxis.PhysicalMinX;
                    double x2 = ls.X2 / (double)(_internalGridSize.Columns - 1) * physicalHorizontalRange + hAxis.PhysicalMinX;
                    double y1 = ls.Y1 / (double)(_internalGridSize.Rows - 1) * physicalVerticalRange + vAxis.PhysicalMinY;
                    double y2 = ls.Y2 / (double)(_internalGridSize.Rows - 1) * physicalVerticalRange + vAxis.PhysicalMinY;

                    FormattedText ft = new FormattedText(contourGroups.First[i].ToString(_contourLabelFormat),
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight, new Typeface("Arial"), 11, _stroke);

                    double angle = Math.Atan2(
                        contourGroups.Second[i][bestJ].Y1 - contourGroups.Second[i][bestJ].Y2,
                        contourGroups.Second[i][bestJ].X2 - contourGroups.Second[i][bestJ].X1) / Math.PI * 180.0;

                    if (angle > 90)
                    {
                        angle -= 180;
                    }
                    if (angle < -90)
                    {
                        angle += 180;
                    }

                    RotateTransform rt = new RotateTransform(angle);

                    dc.PushTransform(new TranslateTransform((x1 + x2) / 2.0, (y1 + y2) / 2.0));
                    dc.PushTransform(rt);
                    dc.DrawText(ft, new Point(-ft.Width / 2.0, -ft.Height / 2.0));
                    dc.Pop();
                    dc.Pop();

                    Rect r1 = new Rect(hAxis.PhysicalMinX, vAxis.PhysicalMaxY, hAxis.PhysicalMaxX - hAxis.PhysicalMinX, vAxis.PhysicalMinY - vAxis.PhysicalMaxY);
                    Rect r2 = new Rect((x1 + x2) / 2.0 - ft.Width*1.6/2.0, (y1 + y2) / 2.0 - ft.Height*1.6/2.0, ft.Width*1.6, ft.Height*1.6 );
                    
                    DrawingBrush db = new DrawingBrush();
                    db.ViewboxUnits = BrushMappingMode.Absolute;
                    db.ViewportUnits = BrushMappingMode.Absolute;
                    db.Viewport = r1;
                    db.Viewbox = r1;

                    GeometryGroup gg = new GeometryGroup();
                    gg.FillRule = FillRule.Nonzero;

                    if (r2.Left - r1.Left > 0)
                    {
                        RectangleGeometry rg = new RectangleGeometry(new Rect(r1.Left, r1.Top, r2.Left - r1.Left, r1.Height));
                        gg.Children.Add(rg);
                    }
                    if (r1.Right - r2.Right > 0)
                    {
                        RectangleGeometry rg = new RectangleGeometry(new Rect(r2.Right, r1.Top, r1.Right - r2.Right, r1.Height));
                        gg.Children.Add(rg);
                    }
                    if ( r2.Top - r1.Top > 0)
                    {
                        RectangleGeometry rg = new RectangleGeometry(new Rect(r1.Left, r1.Top, r1.Width, r2.Top-r1.Top));
                        gg.Children.Add(rg);
                    }
                    if ( r1.Bottom - r2.Bottom > 0)
                    {
                        RectangleGeometry rg = new RectangleGeometry(new Rect(r1.Left, r2.Bottom, r1.Width, r1.Bottom-r2.Bottom));
                        gg.Children.Add(rg);
                    }

                    SolidColorBrush scb = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    GeometryDrawing gd = new GeometryDrawing(scb, null, gg);
                    db.Drawing = gd;

                    dc.PushOpacityMask(db);             
                    
                }

                for (int j = 0; j < lineSegments.Count; ++j)
                {
                    LineSegment ls = lineSegments[j];

                    double x1 = ls.X1 / (double)(_internalGridSize.Columns-1) * physicalHorizontalRange + hAxis.PhysicalMinX;
                    double x2 = ls.X2 / (double)(_internalGridSize.Columns-1) * physicalHorizontalRange + hAxis.PhysicalMinX;
                    double y1 = ls.Y1 / (double)(_internalGridSize.Rows-1) * physicalVerticalRange + vAxis.PhysicalMinY;
                    double y2 = ls.Y2 / (double)(_internalGridSize.Rows-1) * physicalVerticalRange + vAxis.PhysicalMinY;

                    dc.DrawLine(p, new Point(x1, y1), new Point(x2, y2));
                }

                if (drawingLabel)
                {
                    // pop opacity mask.
                    dc.Pop();
                }

            }
        }


        /// <summary>
        /// If true, contour labels will be drawn. If false, they won't.
        /// </summary>
        public bool ShowContourLabels
        {
            get
            {
                return _showContourLabels;
            }
            set
            {
                _showContourLabels = value;
            }
        }
        private bool _showContourLabels = true;


        /// <summary>
        /// Format string used to draw contour labels.
        /// </summary>
        public string ContourLabelFormat
        {
            get
            {
                return _contourLabelFormat;
            }
            set
            {
                _contourLabelFormat = value;
            }
        }
        private string _contourLabelFormat = "0";


        /// <summary>
        /// The size of the internal grid used to calculate contours. The bigger
        /// the grid, the smoother the contours, but the more time they take to
        /// generate.
        /// </summary>
        public Dimensions InternalGridSize
        {
            get
            {
                return _internalGridSize;
            }
            set
            {
                _internalGridSize = value;
                _grid = new double[_internalGridSize.Rows, _internalGridSize.Columns];
            }
        }
        private Dimensions _internalGridSize;


        /// <summary>
        /// The field method to draw contours for.
        /// </summary>
        public FieldDelegate FieldMethod
        {
            get
            {
                return _fieldMethod;
            }
        }
        private FieldDelegate _fieldMethod;


        /// <summary>
        /// The contour levels. 
        /// </summary>
        public List<double> Levels
        {
            get
            {
                return _levels;
            }
        }
        private List<double> _levels;


        /// <summary>
        /// used internally. the field method sampled at internal grid points.
        /// </summary>
        private double[,] _grid;


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
        private Brush _stroke = Brushes.Black;


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

    }
}
