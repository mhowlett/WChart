using System.Windows.Media;
using WChart;

namespace WChartTest
{
	public class LinearAxis_VisualTest_Epsilon : System.Windows.Controls.Control
	{
	    protected override void OnRender(DrawingContext dc)
		{
			GuidelineSet gs = new GuidelineSet(new[] { 0.5 }, new[] { 0.5 });
			dc.PushGuidelineSet(gs);

			new VerticalPhysicalAxis(new LinearAxis(-0.1, 10.1), 300, 10, 50).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(-0.1, 9.99), 300, 10, 100).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(-0.1, 9.9), 300, 10, 150).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(-0.1, 9.89), 300, 10, 200).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0 - Utils.Epsilon/2.0, 10.1), 300, 10, 250).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0 - Utils.Epsilon, 10.1), 300, 10, 300).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0 + Utils.Epsilon*0.99, 10.1), 300, 10, 350).Draw(dc);
			new VerticalPhysicalAxis(new LinearAxis(0 + Utils.Epsilon*1.1, 10.1), 300, 10, 400).Draw(dc);

			dc.Pop();

			base.OnRender(dc);
		}
	}
}