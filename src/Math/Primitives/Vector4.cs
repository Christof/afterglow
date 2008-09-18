using System;

namespace TheNewEngine.Math.Primitives
{
    /// <summary>
    /// 4-dimensional vector which has properties for each axis (x-axis, y-axis, z-axis, w-axis).
    /// </summary>
    /// <remarks>
    /// The coordinate system which is used throughout the engine is right handed which means
    /// that the positive x-axis points right, the positive y-axis points up and
    /// the positive z-axis points forward.
    /// </remarks>
    public struct Vector4
    {
        // Don't use auto properties because then the 
        // default StructLayout (which is LayoutKind.Sequential for structs) is not guaranteed.
        private float mX;

        private float mY;

        private float mZ;

        private float mW;

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
        /// Gets or sets the value for the X-axis.
        /// </summary>
        /// <value>The value for the X-axis.</value>
        public float X
        {
            get { return mX; }
            set { mX = value; }
        }

        /// <summary>
        /// Gets or sets the value for the Y-axis.
        /// </summary>
        /// <value>The value for the Y-axis.</value>
        public float Y
        {
            get { return mY; }
            set { mY = value; }
        }

        /// <summary>
        /// Gets or sets the value for the Z-axis.
        /// </summary>
        /// <value>The value for the Z-axis.</value>
        public float Z
        {
            get { return mZ; }
            set { mZ = value; }
        }

        /// <summary>
        /// Gets or sets the value for the W-axis.
        /// </summary>
        /// <value>The value for the W-axis.</value>
        public float W
        {
            get { return mW; }
            set { mW = value; }
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
    }
}