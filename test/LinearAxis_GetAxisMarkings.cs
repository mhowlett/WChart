using System.Collections.Generic;
using NUnit.Framework;

namespace WChartTest
{
    [TestFixture(Description="Tests the GetAxisMarkings method of LinearAxis")]
    public class LinearAxis_GetAxisMarkings
    {
        [Test(Description="Tests no ticks are placed outside the physical extents of the axis")]
        public void TestA()
        {
            double[] worldMins = { -100, 32.4, 0.0, 0.0000001, 3.0 };
            double[] worldMaxs = { 100, 42.3, 42.3, 0.00004, 900 };
            for (int i = 0; i < worldMins.Length; ++i)
            {
                WChart.LinearAxis la = new WChart.LinearAxis(worldMins[i], worldMaxs[i]);
                List<WChart.AxisMarking> axisMarkings = la.GetAxisMarkings(0.0, 1.0);
                for (int j = 0; j < axisMarkings.Count; ++j)
                {
                    Assert.IsTrue(axisMarkings[j].World > worldMins[i] - WChart.Utils.Epsilon);
                    Assert.IsTrue(axisMarkings[j].World < worldMaxs[i] + WChart.Utils.Epsilon);
                }
            }
        }
    }
}
