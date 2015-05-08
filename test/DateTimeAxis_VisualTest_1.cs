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
	class DateTimeAxis_VisualTest_1 : System.Windows.Controls.Control
	{
		protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new double[] { 0.5 }, new double[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new DateTimeAxis( new DateTime(2007,2,2), new DateTime(2007,2,3)), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 4)), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 5)), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 6)), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 7)), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 8)), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 9)), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 10)), 300, 10, 400).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 11)), 300, 10, 450).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 12)), 300, 10, 500).Draw(dc);
			new VerticalPhysicalAxis(new DateTimeAxis(new DateTime(2007, 2, 2), new DateTime(2007, 2, 13)), 300, 10, 550).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}
