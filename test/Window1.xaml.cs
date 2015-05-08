using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WChartTest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
            ContentRendered += new EventHandler(Window1_ContentRendered);
        }

        void Window1_ContentRendered(object sender, EventArgs e)
        {
            Visual v = GetVisualChild(0);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(new Pen(Brushes.Black, 1.0), new Point(10, 10), new Point(100, 100));
            //base.OnRender(drawingContext);
        }
    }
}