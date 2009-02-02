namespace TheNewEngine.Math
{
    /// <summary>
    /// Square root interpolator.
    /// </summary>
    public class SquareRootInterpolator : IInterpolator
    {
        private readonly float mStartValue;

        private readonly float mEndValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareRootInterpolator"/> class.
        /// </summary>
        /// <param name="startValue">The start value.</param>
        /// <param name="endValue">The end value.</param>
        public SquareRootInterpolator(float startValue, float endValue)
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
            return mStartValue + (mEndValue - mStartValue) * Functions.Sqrt(percentage);
        }
    }
}