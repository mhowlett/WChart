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


	public class LinearAxis_VisualTest_31_50 : System.Windows.Controls.Control
	{
		static LinearAxis_VisualTest_31_50()
		{
		}

		protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new double[] { 0.5 }, new double[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new LinearAxis(0, 31), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 32), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 33), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 34), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 35), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 36), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 37), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 38), 300, 10, 400).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 39), 300, 10, 450).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 40), 300, 10, 500).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 41), 300, 10, 550).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 42), 300, 10, 600).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 43), 300, 10, 650).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 44), 300, 10, 700).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 45), 300, 10, 750).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 46), 300, 10, 800).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 47), 300, 10, 850).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 48), 300, 10, 900).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 49), 300, 10, 950).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0, 50), 300, 10, 1000).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}
