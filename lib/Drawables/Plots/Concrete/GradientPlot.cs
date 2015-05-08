using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WChart
{
    public class GradientPlot : IPlot
    {

        public GradientPlot(FieldDelegate fieldMethod, Dimensions internalGridSize, List<Pair<double,Color>> gradientStops )
        {
            _fieldMethod = fieldMethod;
            _internalGridSize = internalGridSize;
            _gradientStops = new List<Pair<double,Color>>( gradientStops );
            _gradientStops.Sort(
                    delegate( Pair<double,Color> a, Pair<double,Color> b)
                    {
                        if (a.First > b.First)
                        {
                            return 1;
                        }
                        if (a.First == b.First)
                        {
                            return 0;
                        }
                        return -1;
                    }
                );
        }


        public Axis SuggestedAxisX()
        {
            throw new NotImplementedException();
        }


        public Axis SuggestedAxisY()
        {
            throw new NotImplementedException();
        }

        public void AddToLegend(Legend legend)
        {
            throw new NotImplementedException();
        }

        public void Draw(System.Windows.Media.DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis)
        {
            double worldHorizontalRange = hAxis.Axis.WorldMax - hAxis.Axis.WorldMin;
            double worldVerticalRange = vAxis.Axis.WorldMax - vAxis.Axis.WorldMin;
            double physicalHorizontalRange = Math.Abs(hAxis.PhysicalMaxX - hAxis.PhysicalMinX);
            double physicalVerticalRange = Math.Abs(vAxis.PhysicalMaxY - vAxis.PhysicalMinY);

            double worldBoxWidth = worldHorizontalRange / _internalGridSize.Columns;
            double worldBoxHeight = worldVerticalRange / _internalGridSize.Rows;
            double physicalBoxWidth = physicalHorizontalRange / _internalGridSize.Columns + 1.0;
            double physicalBoxHeight = physicalVerticalRange / _internalGridSize.Rows + 1.0;

            byte[] img = new byte[_internalGridSize.Rows * _internalGridSize.Columns * 4];
            for (int i = 0; i < _internalGridSize.Rows; ++i)
            {
                for (int j = 0; j < _internalGridSize.Columns; ++j)
                {
                    double worldY = (double)i / (double)(_internalGridSize.Rows) * worldVerticalRange + vAxis.Axis.WorldMin + worldBoxHeight / 2.0;
                    double worldX = (double)j / (double)(_internalGridSize.Columns) * worldHorizontalRange + hAxis.Axis.WorldMin + worldBoxWidth / 2.0;
                    double val = FieldMethod(worldX, worldY);

                    int ind = -1;
                    for (int k = 0; k < _gradientStops.Count; ++k)
                    {
                        if (_gradientStops[k].First > val)
                        {
                            ind = k;
                            break;
                        }
                    }

                    Color c;
                    if (ind == -1)
                    {
                        c = _gradientStops[_gradientStops.Count - 1].Second;
                    }
                    else if (ind == 0)
                    {
                        c = _gradientStops[0].Second;
                    }
                    else
                    {
                        Color c1 = _gradientStops[ind - 1].Second;
                        double gv1 = _gradientStops[ind - 1].First;
                        Color c2 = _gradientStops[ind].Second;
                        double gv2 = _gradientStops[ind].First;
                        System.Diagnostics.Debug.Assert(val >= gv1 && val < gv2);
                        double prop = (val - gv1) / (gv2 - gv1);
                        c = Color.FromRgb(
                            (byte)(c1.R * (1.0 - prop) + c2.R*prop),
                            (byte)(c1.G * (1.0 - prop) + c2.G*prop),
                            (byte)(c1.B * (1.0 - prop) + c2.B*prop));
                    }

                    img[ (_internalGridSize.Rows - 1 - i) * _internalGridSize.Columns * 4 + j * 4] = c.R;
                    img[ (_internalGridSize.Rows - 1 - i) * _internalGridSize.Columns * 4 + j * 4 + 1] = c.G;
                    img[ (_internalGridSize.Rows - 1 - i) * _internalGridSize.Columns * 4 + j * 4 + 2] = c.B;
                }
            }
            BitmapSource bs = BitmapSource.Create(_internalGridSize.Columns, _internalGridSize.Rows, 96, 96, PixelFormats.Bgr32, null, img, _internalGridSize.Columns*4);
           
            dc.DrawImage(bs, new Rect(hAxis.PhysicalMinX, vAxis.PhysicalMaxY, physicalHorizontalRange, physicalVerticalRange));

            /*
            for (int i = 0; i < _internalGridSize.Rows; ++i)
            {
                for (int j = 0; j < _internalGridSize.Columns; ++j)
                {
                    double worldY = (double)i / (double)(_internalGridSize.Rows) * worldVerticalRange + vAxis.Axis.WorldMin + worldBoxHeight / 2.0;
                    double worldX = (double)j / (double)(_internalGridSize.Columns) * worldHorizontalRange + hAxis.Axis.WorldMin + worldBoxWidth / 2.0;

                    Color c = Color.FromRgb(0, 0, 0);
                    for (int k = 0; k < _gradientStops.Count; ++k)
                    {
                        if (_gradientStops[k].First > FieldMethod(worldX, worldY))
                        {
                            break;
                        }
                        c = _gradientStops[k].Second;
                    }
                   
                    dc.DrawRectangle(b, null,
                        new Rect(
                            hAxis.WorldToPhysical(worldX, ClippingType.Clip) - physicalBoxWidth / 2.0,
                            vAxis.WorldToPhysical(worldY, ClippingType.Clip) - physicalBoxHeight / 2.0,
                            physicalBoxWidth, physicalBoxHeight));
                    
                }
            }
            */

            
        }

        public Dimensions InternalGridSize
        {
            get
            {
                return _internalGridSize;
            }
        }
        private Dimensions _internalGridSize;


        public FieldDelegate FieldMethod
        {
            get
            {
                return _fieldMethod;
            }
        }
        private FieldDelegate _fieldMethod;

        public List<Pair<double,Color>> GradientStops
        {
            get
            {
                return _gradientStops;
            }
        }
        private List<Pair<double,Color>> _gradientStops;

    }
}
