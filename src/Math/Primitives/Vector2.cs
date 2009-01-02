using System.Globalization;

namespace TheNewEngine.Math
{
    /// <summary>
    /// 2-dimensional vector which has properties for each axis (x-axis, y-axis).
    /// </summary>
    public struct Vector2
    {
        private static readonly Vector2 ZERO = new Vector2(0, 0);

        private static readonly Vector2 X_AXIS = new Vector2(1, 0);

        private static readonly Vector2 Y_AXIS = new Vector2(0, 1);

        // Don't use auto properties because then the 
        // default StructLayout (which is LayoutKind.Sequential for structs) is not guaranteed.
        private readonly float mX;

        private readonly float mY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="x">The value for the X-axis.</param>
        /// <param name="y">The value for the Y-axis.</param>
        public Vector2(float x, float y)
        {
            mX = x;
            mY = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="values">The values.</param>
        public Vector2(float[] values)
        {
            mX = values[0];
            mY = values[1];
        }

        /// <summary>
        /// Gets the value for the X-axis.
        /// </summary>
        /// <value>The value for the X-axis.</value>
        public float X
        {
            get { return mX; }
        }

        /// <summary>
        /// Gets the value for the Y-axis.
        /// </summary>
        /// <value>The value for the Y-axis.</value>
        public float Y
        {
            get { return mY; }
        }

        /// <summary>
        /// Gets a zero initialized vector.
        /// </summary>
        /// <value>Zero initialized vector.</value>
        public static Vector2 Zero
        {
            get { return ZERO; }
        }

        /// <summary>
        /// Gets the X axis vector.
        /// </summary>
        /// <value>The X axis.</value>
        public static Vector2 XAxis
        {
            get { return X_AXIS; }
        }

        /// <summary>
        /// Gets the Y axis vector.
        /// </summary>
        /// <value>The Y axis.</value>
        public static Vector2 YAxis
        {
            get { return Y_AXIS; }
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "X: {0} Y: {1}", mX, mY);
        }
    }
}