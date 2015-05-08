
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
    public class StepPlot : IPlot
    {

        public Axis SuggestedAxisX()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Axis SuggestedAxisY()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddToLegend(Legend legend)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Draw(System.Windows.Media.DrawingContext dc, HorizontalPhysicalAxis hAxis, VerticalPhysicalAxis vAxis)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
