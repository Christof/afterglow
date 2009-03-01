namespace Afterglow.Math
{
    /// <summary>
    /// Provides an Equals method which compares under the consideration of a given delta value.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public interface IDeltaEquatable<T>
    {
        /// <summary>
        /// Indicates whether this instance and a specified object are equal under the consideration
        /// of the given delta value.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>
        /// true if <paramref name="other"/> and this instance have the same value; otherwise, false.
        /// </returns>
        bool Equals(T other, float delta);
    }
}