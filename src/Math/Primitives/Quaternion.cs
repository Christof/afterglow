namespace Afterglow.Math
{
    /// <summary>
    /// Represents a rotation in 3d-space with four values (a rotation axis and an angle).
    /// </summary>
    public class Quaternion
    {
        private Vector4 mValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="destinationPosition">The destination position.</param>
        public Quaternion(Vector3 sourcePosition, Vector3 destinationPosition)
        {
            var r = sourcePosition.Cross(destinationPosition);
            var s = Functions.Sqrt(2 * (1 + sourcePosition.Dot(destinationPosition)));

            mValues = new Vector4(r / s, s / 2);
        }

        /// <summary>
        /// Gets the X value.
        /// </summary>
        /// <value>The X value.</value>
        public float X
        {
            get { return mValues.X; }
        }

        /// <summary>
        /// Gets the Y value.
        /// </summary>
        /// <value>The Y value.</value>
        public float Y
        {
            get { return mValues.Y; }
        }

        /// <summary>
        /// Gets the Z value.
        /// </summary>
        /// <value>The Z value.</value>
        public float Z
        {
            get { return mValues.Z; }
        }

        /// <summary>
        /// Gets the W value.
        /// </summary>
        /// <value>The W value.</value>
        public float W
        {
            get { return mValues.W; }
        }
    }
}