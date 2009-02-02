namespace TheNewEngine.Math
{
    /// <summary>
    /// Interface for all interpolators. The interface contains
    /// only the <see cref="Interpolate"/>-method because the
    /// setup for interpolator differs on each interpolation
    /// method used.
    /// </summary>
    public interface IInterpolator
    {
        /// <summary>
        /// Interpolates a value for the specified percentage.
        /// </summary>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The interpolated value.</returns>
        float Interpolate(float percentage);
    }
}