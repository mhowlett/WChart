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

using WChart;

namespace WChartTest
{


	public class LinearAxis_VisualTest_91_110 : System.Windows.Controls.Control
	{
		static LinearAxis_VisualTest_91_110()
		{
		}

		protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new double[] { 0.5 }, new double[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new LinearAxis(0, 91), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 92), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 93), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 94), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 95), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 96), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 97), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 98), 300, 10, 400).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 99), 300, 10, 450).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 100), 300, 10, 500).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 101), 300, 10, 550).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 102), 300, 10, 600).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 103), 300, 10, 650).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 104), 300, 10, 700).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 105), 300, 10, 750).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 106), 300, 10, 800).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 107), 300, 10, 850).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 108), 300, 10, 900).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 109), 300, 10, 950).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 110), 300, 10, 1000).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}
