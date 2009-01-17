namespace TheNewEngine.Math
{
    /// <summary>
    /// Contains extension methods.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Indicates whether the two floats are equal under the consideration
        /// of the given delta value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="other">The other.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>
        /// true if <paramref name="other"/> and this float have the same value; otherwise, false.
        /// </returns>
        public static bool EqualsWithDelta(this float value, float other, float delta)
        {
            return Functions.Abs(value - other) <= delta;
        }

        /// <summary>
        /// Indicates whether the two floats are equal under the consideration
        /// of <see cref="Constants.DELTA"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="other">The other.</param>
        /// <returns>
        /// true if <paramref name="other"/> and this float have the same value; otherwise, false.
        /// </returns>
        public static bool EqualsWithDelta(this float value, float other)
        {
            return value.EqualsWithDelta(other, Constants.DELTA);
        }

        /// <summary>
        /// Clamps the value to the range given by min and max.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(this float value, float min, float max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }

        /// <summary>
        /// Clamps the value to the given maximum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The clamped value.</returns>
        public static float ClampMax(this float value, float max)
        {
            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// Clamps the value to the given minimum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <returns>The clamped value.</returns>
        public static float ClampMin(this float value, float min)
        {
            if (value < min)
                return min;

            return value;
        }
    }
}