using MbUnit.Framework;

namespace TheNewEngine.Math.Interpolators
{
    public class Test_SquareRootInterpolator
    {
        public const float ONE_DIVIDED_BY_SQRT_OF_TWO = 0.7071068f;

        [Test]
        [Row(0.0f, 0.0f, Description = "0% should return the start value")]
        [Row(0.5f, ONE_DIVIDED_BY_SQRT_OF_TWO * 10.0f, Description = "50% should return squareroot interpolated value")]
        [Row(1.0f, 10.0f, Description = "100% should return the end value")]
        [Row(-1.0f, 10.0f, Description = "-100% should return the end value")]
        [Row(2.0f, ONE_DIVIDED_BY_SQRT_OF_TWO * 20.0f, Description = "200% should return four times the end value")]
        public void Interpolate(float percentage, float interpolatedValue)
        {
            var startValue = 0.0f;
            var endValue = 10.0f;
            var interpolator = new SquareRootInterpolator(startValue, endValue);

            Assert.AreApproximatelyEqual(interpolatedValue, 
                interpolator.Interpolate(percentage),
                Constants.DELTA);
        }
    }
}