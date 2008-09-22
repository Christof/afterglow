namespace TheNewEngine.Math
{
    /// <summary>
    /// Contains extension methods.
    /// </summary>
    public static class Extensions
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
    }
}