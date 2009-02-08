using MbUnit.Framework;
using TheNewEngine.Infrastructure;

namespace TheNewEngine.Math
{
    public class Test_Constants
    {
        private const float DELTA = 0.000001f;

        private const float PI = (float)System.Math.PI;

        private const float PI_HALF = PI * 0.5f;

        [Test]
        [Row(PI, Constants.PI, Description = "Pi")]
        [Row(PI * 2, Constants.TWO_PI, Description = "2 Pi")]
        [Row(PI_HALF, Constants.HALF_PI, Description = "0.5 Pi")]
        public void Constant_values(float expected, float actual)
        {
            actual.ShouldEqual(expected, DELTA);
        }
    }
}