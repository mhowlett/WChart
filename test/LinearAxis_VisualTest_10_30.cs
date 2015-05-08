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
	public class LinearAxis_VisualTest_10_30 : System.Windows.Controls.Control
	{
		static LinearAxis_VisualTest_10_30()
		{
		}

		protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new double[] { 0.5 }, new double[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new LinearAxis(0, 10), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 11), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 12), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 13), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 14), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 15), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 16), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 17), 300, 10, 400).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 18), 300, 10, 450).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 19), 300, 10, 500).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 21), 300, 10, 550).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 22), 300, 10, 600).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 23), 300, 10, 650).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 24), 300, 10, 700).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 25), 300, 10, 750).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 26), 300, 10, 800).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 27), 300, 10, 850).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 28), 300, 10, 900).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 29), 300, 10, 950).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 30), 300, 10, 1000).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}
