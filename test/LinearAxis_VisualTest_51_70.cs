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


	public class LinearAxis_VisualTest_51_70 : System.Windows.Controls.Control
	{
		static LinearAxis_VisualTest_51_70()
		{
		}

		protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new double[] { 0.5 }, new double[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new LinearAxis(0, 51), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 52), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 53), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 54), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 55), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 56), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 57), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 58), 300, 10, 400).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 59), 300, 10, 450).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 60), 300, 10, 500).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 61), 300, 10, 550).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 62), 300, 10, 600).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 63), 300, 10, 650).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 64), 300, 10, 700).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 65), 300, 10, 750).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 66), 300, 10, 800).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 67), 300, 10, 850).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 68), 300, 10, 900).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 69), 300, 10, 950).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 70), 300, 10, 1000).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}
