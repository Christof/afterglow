using MbUnit.Framework;

namespace TheNewEngine.Math
{
    public class Test_LinearInterpolator
    {
        [Test]
        [Row(0.0f, 0.0f, Description = "0% should return the start value")]
        [Row(0.5f, 5.0f, Description = "50% should return average of start and end value")]
        [Row(1.0f, 10.0f, Description = "100% should return the end value")]
        [Row(-1.0f, -10.0f, Description = "-100% should return the negated end value")]
        [Row(2.0f, 20.0f, Description = "200% should return two times the end value")]
        public void Interpolate(float percentage,  float interpolatedValue)
        {
            var startValue = 0.0f;
            var endValue = 10.0f;
            var interpolator = new LinearInterpolator(startValue, endValue);

            Assert.AreEqual(interpolatedValue, interpolator.Interpolate(percentage));
        }
    }
}