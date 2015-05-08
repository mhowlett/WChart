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


	public class LinearAxis_VisualTest_71_90 : System.Windows.Controls.Control
	{
		static LinearAxis_VisualTest_71_90()
		{
		}

		protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new double[] { 0.5 }, new double[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new LinearAxis(0, 71), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 72), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 73), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 74), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 75), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 76), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 77), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 78), 300, 10, 400).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 79), 300, 10, 450).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 80), 300, 10, 500).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 81), 300, 10, 550).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 82), 300, 10, 600).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 83), 300, 10, 650).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 84), 300, 10, 700).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 85), 300, 10, 750).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 86), 300, 10, 800).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 87), 300, 10, 850).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 88), 300, 10, 900).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 89), 300, 10, 950).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 90), 300, 10, 1000).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}
