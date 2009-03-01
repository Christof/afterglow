using System.Collections.Generic;

namespace Afterglow.Math
{
    /// <summary>
    /// Equality comparer for two <see cref="Vector3"/>.
    /// </summary>
    public class Vector3EqualityComparer : IEqualityComparer<Vector3>
    {
        private readonly float mDelta;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3EqualityComparer"/> class.
        /// </summary>
        /// <param name="delta">The delta.</param>
        public Vector3EqualityComparer(float delta)
        {
            mDelta = delta;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3EqualityComparer"/> class.
        /// </summary>
        public Vector3EqualityComparer()
            : this(Constants.DELTA)
        {
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="firstVector">
        /// The first vector to compare.
        /// </param>
        /// <param name="secondVector">
        /// The second vector to compare.
        /// </param>
        public bool Equals(Vector3 firstVector, Vector3 secondVector)
        {
            return firstVector.X.EqualsWithDelta(secondVector.X, mDelta) &&
                firstVector.Y.EqualsWithDelta(secondVector.Y, mDelta) &&
                firstVector.Z.EqualsWithDelta(secondVector.Z, mDelta);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object" /> for which a hash code is to be returned.
        /// </param>
        public int GetHashCode(Vector3 obj)
        {
            return obj.GetHashCode();
        }
    }
}