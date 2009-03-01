namespace Afterglow.Math
{
    /// <summary>
    /// Square interpolator
    /// </summary>
    public class SquareInterpolator : IInterpolator
    {
        private readonly float mStartValue;

        private readonly float mEndValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareInterpolator"/> class.
        /// </summary>
        /// <param name="startValue">The start value.</param>
        /// <param name="endValue">The end value.</param>
        public SquareInterpolator(float startValue, float endValue)
        {
            mStartValue = startValue;
            mEndValue = endValue;
        }

        /// <summary>
        /// Interpolates a value for the specified percentage.
        /// </summary>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The interpolated value.</returns>
        public float Interpolate(float percentage)
        {
            return mStartValue + (mEndValue - mStartValue) * percentage * percentage;
        }
    }
}