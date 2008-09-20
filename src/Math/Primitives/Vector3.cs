using System;

namespace TheNewEngine.Math.Primitives
{
    /// <summary>
    /// 3-dimensional vector which has properties for each axis (x-axis, y-axis, z-axis).
    /// </summary>
    /// <remarks>
    /// The coordinate system which is used throughout the engine is right handed which means
    /// that the positive x-axis points right, the positive y-axis points up and
    /// the positive z-axis points forward.
    /// </remarks>
    public struct Vector3
    {
        // Don't use auto properties because then the 
        // default StructLayout (which is LayoutKind.Sequential for structs) is not guaranteed.
        private float mX;

        private float mY;

        private float mZ;

        private static readonly Vector3 ZERO = new Vector3(0, 0, 0);

        private static readonly Vector3 X_AXIS = new Vector3(1, 0, 0);

        private static readonly Vector3 Y_AXIS = new Vector3(0, 1, 0);

        private static readonly Vector3 Z_AXIS = new Vector3(0, 0, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="x">The value for the X-axis.</param>
        /// <param name="y">The value for the Y-axis.</param>
        /// <param name="z">The value for the Z-axis.</param>
        public Vector3(float x, float y, float z)
        {
            mX = x;
            mY = y;
            mZ = z;
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
        /// Gets a zero initialized vector.
        /// </summary>
        /// <value>Zero initialized vector.</value>
        public static Vector3 Zero
        {
            get { return ZERO; }
        }

        /// <summary>
        /// Gets the X axis vector.
        /// </summary>
        /// <value>The X axis.</value>
        public static Vector3 XAxis
        {
            get { return X_AXIS; }
        }

        /// <summary>
        /// Gets the Y axis vector.
        /// </summary>
        /// <value>The Y axis.</value>
        public static Vector3 YAxis
        {
            get { return Y_AXIS; }
        }

        /// <summary>
        /// Gets the Z axis vector.
        /// </summary>
        /// <value>The Z axis.</value>
        public static Vector3 ZAxis
        {
            get { return Z_AXIS; }
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        /// <value>The squared length.</value>
        public float LengthSquared
        {
            get { return mX * mX + mY * mY + mZ * mZ; }
        }

        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        /// <value>The length.</value>
        public float Length
        {
            get { return (float)System.Math.Sqrt(LengthSquared); }
        }

        /// <summary>
        /// Gets the value at the specified axis.
        /// </summary>
        /// <value>Value of the specified axis.</value>
        /// <param name="index">0 -> x-axis; 1 -> y-axis; 2 -> z-axis.</param>
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
                    default: throw new IndexOutOfRangeException(
                        "The Vector3 has no value at the given index");
                }
            }
        }

        /// <summary>
        /// Normalizes the vector.
        /// <remarks>
        /// A normalized vector has a length of 1.
        /// </remarks>
        /// </summary>
        public void Normalize()
        {
            float length = Length;
            mX /= length;
            mY /= length;
            mZ /= length;
        }

        /// <summary>
        /// Returns the cross product of this vector with the given one.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>The cross product.</returns>
        public Vector3 Cross(Vector3 other)
        {
            return new Vector3(
                mY * other.Z - mZ * other.Y,
                -mX * other.Z + mZ * other.X,
                mX * other.Y - mY * other.X);
        }

        /// <summary>
        /// Implements the operator + which adds up two vectors.
        /// </summary>
        /// <param name="summand1">The first summand.</param>
        /// <param name="summand2">The second summand.</param>
        /// <returns>The sum of the two summands.</returns>
        public static Vector3 operator+(Vector3 summand1, Vector3 summand2)
        {
            return new Vector3(
                summand1.X + summand2.X,
                summand1.Y + summand2.Y,
                summand1.Z + summand2.Z);
        }
    }
}