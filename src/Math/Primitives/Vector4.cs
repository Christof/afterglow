using System;
using System.Globalization;

namespace Afterglow.Math
{
    /// <summary>
    /// 4-dimensional vector which has properties for each axis (x-axis, y-axis, z-axis, w-axis).
    /// </summary>
    /// <remarks>
    /// The coordinate system which is used throughout the engine is right handed which means
    /// that the positive x-axis points right, the positive y-axis points up and
    /// the positive z-axis points forward.
    /// </remarks>
    [Serializable]
    public struct Vector4
    {
        // Don't use auto properties because then the 
        // default StructLayout (which is LayoutKind.Sequential for structs) is not guaranteed.
        private readonly float mX;

        private readonly float mY;

        private readonly float mZ;

        private readonly float mW;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="x">The value for the X-axis.</param>
        /// <param name="y">The value for the Y-axis.</param>
        /// <param name="z">The value for the Z-axis.</param>
        /// <param name="w">The value for the W-axis.</param>
        public Vector4(float x, float y, float z, float w)
        {
            mX = x;
            mY = y;
            mZ = z;
            mW = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> struct.
        /// </summary>
        /// <param name="values">The values.</param>
        public Vector4(float[] values)
        {
            mX = values[0];
            mY = values[1];
            mZ = values[2];
            mW = values[3];
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
        /// Gets the value for the Z-axis.
        /// </summary>
        /// <value>The value for the Z-axis.</value>
        public float Z
        {
            get { return mZ; }
        }

        /// <summary>
        /// Gets the value for the W-axis.
        /// </summary>
        /// <value>The value for the W-axis.</value>
        public float W
        {
            get { return mW; }
        }

        /// <summary>
        /// Gets the value at the specified axis.
        /// </summary>
        /// <value>Value of the specified axis.</value>
        /// <param name="index">0 -> x-axis; 1 -> y-axis; 2 -> z-axis; 3 -> w-axis.</param>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return mX;
                    case 1:
                        return mY;
                    case 2:
                        return mZ;
                    case 3:
                        return mW;
                    default: throw new IndexOutOfRangeException(
                        "The Vector3 has no value at the given index");
                }
            }
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
            return string.Format(CultureInfo.InvariantCulture, 
                "X: {0} Y: {1} Z: {2} W: {3}", mX, mY, mZ, mW);
        }
    }
}