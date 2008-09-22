using System;

namespace TheNewEngine.Math.Primitives
{
    /// <summary>
    /// 4x4 Matrix which can represent transformations like translation, scale, rotation.
    /// <remarks>
    /// The implementation is a row major matrix.
    /// </remarks>
    /// </summary>
    public struct Matrix : ICoordinateSystem, IEquatable<Matrix>, IDeltaEquatable<Matrix>
    {
        // Don't use auto properties because then the 
        // default StructLayout (which is LayoutKind.Sequential for structs) is not guaranteed.
        private float mR1C1;
        private float mR1C2;
        private float mR1C3;
        private float mR1C4;

        private float mR2C1;
        private float mR2C2;
        private float mR2C3;
        private float mR2C4;

        private float mR3C1;
        private float mR3C2;
        private float mR3C3;
        private float mR3C4;

        private float mR4C1;
        private float mR4C2;
        private float mR4C3;
        private float mR4C4;

        private static readonly Matrix IDENTITY = new Matrix(
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// </summary>
        /// <param name="r1c1">The value of the row 1 column 1.</param>
        /// <param name="r1c2">The value of the row 1 column 2.</param>
        /// <param name="r1c3">The value of the row 1 column 3.</param>
        /// <param name="r1c4">The value of the row 1 column 4.</param>
        /// <param name="r2c1">The value of the row 2 column 1.</param>
        /// <param name="r2c2">The value of the row 2 column 2.</param>
        /// <param name="r2c3">The value of the row 2 column 3.</param>
        /// <param name="r2c4">The value of the row 2 column 4.</param>
        /// <param name="r3c1">The value of the row 3 column 1.</param>
        /// <param name="r3c2">The value of the row 3 column 2.</param>
        /// <param name="r3c3">The value of the row 3 column 3.</param>
        /// <param name="r3c4">The value of the row 3 column 4.</param>
        /// <param name="r4c1">The value of the row 4 column 1.</param>
        /// <param name="r4c2">The value of the row 4 column 2.</param>
        /// <param name="r4c3">The value of the row 4 column 3.</param>
        /// <param name="r4c4">The value of the row 4 column 4.</param>
        public Matrix(
            float r1c1, float r1c2, float r1c3, float r1c4,
            float r2c1, float r2c2, float r2c3, float r2c4, 
            float r3c1, float r3c2, float r3c3, float r3c4, 
            float r4c1, float r4c2, float r4c3, float r4c4)
        {
            mR1C1 = r1c1;
            mR1C2 = r1c2;
            mR1C3 = r1c3;
            mR1C4 = r1c4;

            mR2C1 = r2c1;
            mR2C2 = r2c2;
            mR2C3 = r2c3;
            mR2C4 = r2c4;

            mR3C1 = r3c1;
            mR3C2 = r3c2;
            mR3C3 = r3c3;
            mR3C4 = r3c4;

            mR4C1 = r4c1;
            mR4C2 = r4c2;
            mR4C3 = r4c3;
            mR4C4 = r4c4;
        }

        /// <summary>
        /// Gets or sets the value of row 1 column 1.
        /// </summary>
        /// <value>The value of row 1 column 1.</value>
        public float R1C1
        {
            get { return mR1C1; }
            set { mR1C1 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 1 column 2.
        /// </summary>
        /// <value>The value of row 1 column 2.</value>
        public float R1C2
        {
            get { return mR1C2; }
            set { mR1C2 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 1 column 3.
        /// </summary>
        /// <value>The value of row 1 column 3.</value>
        public float R1C3
        {
            get { return mR1C3; }
            set { mR1C3 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 1 column 4.
        /// </summary>
        /// <value>The value of row 1 column 4.</value>
        public float R1C4
        {
            get { return mR1C4; }
            set { mR1C4 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 2 column 1.
        /// </summary>
        /// <value>The value of row 2 column 1.</value>
        public float R2C1
        {
            get { return mR2C1; }
            set { mR2C1 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 2 column 2.
        /// </summary>
        /// <value>The value of row 2 column 2.</value>
        public float R2C2
        {
            get { return mR2C2; }
            set { mR2C2 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 2 column 3.
        /// </summary>
        /// <value>The value of row 2 column 3.</value>
        public float R2C3
        {
            get { return mR2C3; }
            set { mR2C3 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 2 column 4.
        /// </summary>
        /// <value>The value of row 2 column 4.</value>
        public float R2C4
        {
            get { return mR2C4; }
            set { mR2C4 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 3 column 1.
        /// </summary>
        /// <value>The value of row 3 column 1.</value>
        public float R3C1
        {
            get { return mR3C1; }
            set { mR3C1 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 3 column 2.
        /// </summary>
        /// <value>The value of row 3 column 2.</value>
        public float R3C2
        {
            get { return mR3C2; }
            set { mR3C2 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 3 column 3.
        /// </summary>
        /// <value>The value of row 3 column 3.</value>
        public float R3C3
        {
            get { return mR3C3; }
            set { mR3C3 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 3 column 4.
        /// </summary>
        /// <value>The value of row 3 column 4.</value>
        public float R3C4
        {
            get { return mR3C4; }
            set { mR3C4 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 4 column 1.
        /// </summary>
        /// <value>The value of row 4 column 1.</value>
        public float R4C1
        {
            get { return mR4C1; }
            set { mR4C1 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 4 column 2.
        /// </summary>
        /// <value>The value of row 4 column 2.</value>
        public float R4C2
        {
            get { return mR4C2; }
            set { mR4C2 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 4 column 3.
        /// </summary>
        /// <value>The value of row 4 column 3.</value>
        public float R4C3
        {
            get { return mR4C3; }
            set { mR4C3 = value; }
        }

        /// <summary>
        /// Gets or sets the value of row 4 column 4.
        /// </summary>
        /// <value>The value of row 4 column 4.</value>
        public float R4C4
        {
            get { return mR4C4; }
            set { mR4C4 = value; }
        }

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        /// <remarks>
        /// The elements on the main diagonal are 1; all others are 0.
        /// </remarks>
        /// <value>The identity matrix.</value>
        public static Matrix Identity
        {
            get { return IDENTITY; }
        }

        /// <summary>
        /// Gets the X axis.
        /// </summary>
        /// <value>The X axis.</value>
        public Vector3 XAxis
        {
            get { return new Vector3(mR1C1, mR2C1, mR3C1); }
        }

        /// <summary>
        /// Gets the Y axis.
        /// </summary>
        /// <value>The Y axis.</value>
        public Vector3 YAxis
        {
            get { return new Vector3(mR1C2, mR2C2, mR3C2); }
        }

        /// <summary>
        /// Gets the Z axis.
        /// </summary>
        /// <value>The Z axis.</value>
        public Vector3 ZAxis
        {
            get { return new Vector3(mR1C3, mR2C3, mR3C3); }
        }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        /// <value>The origin.</value>
        public Vector3 Origin
        {
            get { return new Vector3(mR1C4, mR2C4, mR3C4); }
        }

        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        public void Transpose()
        {
            var temp = mR1C2;
            mR1C2 = mR2C1;
            mR2C1 = temp;

            temp = mR1C3;
            mR1C3 = mR3C1;
            mR3C1 = temp;

            temp = mR1C4;
            mR1C4 = mR4C1;
            mR4C1 = temp;

            temp = mR2C3;
            mR2C3 = mR3C2;
            mR3C2 = temp;

            temp = mR2C4;
            mR2C4 = mR4C2;
            mR4C2 = temp;

            temp = mR3C4;
            mR3C4 = mR4C3;
            mR4C3 = temp;
        }

        /// <summary>
        /// Multiplies the two given matrices.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>The product of the two given matrices.</returns>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            return new Matrix(
                left.mR1C1 * right.mR1C1 + left.mR1C2 * right.mR2C1 + left.mR1C3 * right.mR3C1 + left.mR1C4 * right.mR4C1,
                left.mR1C1 * right.mR1C2 + left.mR1C2 * right.mR2C2 + left.mR1C3 * right.mR3C2 + left.mR1C4 * right.mR4C2,
                left.mR1C1 * right.mR1C3 + left.mR1C2 * right.mR2C3 + left.mR1C3 * right.mR3C3 + left.mR1C4 * right.mR4C3,
                left.mR1C1 * right.mR1C4 + left.mR1C2 * right.mR2C4 + left.mR1C3 * right.mR3C4 + left.mR1C4 * right.mR4C4,

                left.mR2C1 * right.mR1C1 + left.mR2C2 * right.mR2C1 + left.mR2C3 * right.mR3C1 + left.mR2C4 * right.mR4C1,
                left.mR2C1 * right.mR1C2 + left.mR2C2 * right.mR2C2 + left.mR2C3 * right.mR3C2 + left.mR2C4 * right.mR4C2,
                left.mR2C1 * right.mR1C3 + left.mR2C2 * right.mR2C3 + left.mR2C3 * right.mR3C3 + left.mR2C4 * right.mR4C3,
                left.mR2C1 * right.mR1C4 + left.mR2C2 * right.mR2C4 + left.mR2C3 * right.mR3C4 + left.mR2C4 * right.mR4C4,

                left.mR3C1 * right.mR1C1 + left.mR3C2 * right.mR2C1 + left.mR3C3 * right.mR3C1 + left.mR3C4 * right.mR4C1,
                left.mR3C1 * right.mR1C2 + left.mR3C2 * right.mR2C2 + left.mR3C3 * right.mR3C2 + left.mR3C4 * right.mR4C2,
                left.mR3C1 * right.mR1C3 + left.mR3C2 * right.mR2C3 + left.mR3C3 * right.mR3C3 + left.mR3C4 * right.mR4C3,
                left.mR3C1 * right.mR1C4 + left.mR3C2 * right.mR2C4 + left.mR3C3 * right.mR3C4 + left.mR3C4 * right.mR4C4,

                left.mR4C1 * right.mR1C1 + left.mR4C2 * right.mR2C1 + left.mR4C3 * right.mR3C1 + left.mR4C4 * right.mR4C1,
                left.mR4C1 * right.mR1C2 + left.mR4C2 * right.mR2C2 + left.mR4C3 * right.mR3C2 + left.mR4C4 * right.mR4C2,
                left.mR4C1 * right.mR1C3 + left.mR4C2 * right.mR2C3 + left.mR4C3 * right.mR3C3 + left.mR4C4 * right.mR4C3,
                left.mR4C1 * right.mR1C4 + left.mR4C2 * right.mR2C4 + left.mR4C3 * right.mR3C4 + left.mR4C4 * right.mR4C4);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal under the consideration
        /// of the given delta value.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>
        /// true if <paramref name="other"/> and this instance have the same value; otherwise, false.
        /// </returns>
        public bool Equals(Matrix other, float delta)
        {
            return 
                mR1C1.EqualsWithDelta(other.mR1C1, delta) &&
                mR1C2.EqualsWithDelta(other.mR1C2, delta) &&
                mR1C3.EqualsWithDelta(other.mR1C3, delta) &&
                mR1C4.EqualsWithDelta(other.mR1C4, delta) &&
                
                mR2C1.EqualsWithDelta(other.mR2C1, delta) &&
                mR2C2.EqualsWithDelta(other.mR2C2, delta) &&
                mR2C3.EqualsWithDelta(other.mR2C3, delta) &&
                mR2C4.EqualsWithDelta(other.mR2C4, delta) &&
                
                mR3C1.EqualsWithDelta(other.mR3C1, delta) &&
                mR3C2.EqualsWithDelta(other.mR3C2, delta) &&
                mR3C3.EqualsWithDelta(other.mR3C3, delta) &&
                mR3C4.EqualsWithDelta(other.mR3C4, delta) &&
                
                mR4C1.EqualsWithDelta(other.mR4C1, delta) &&
                mR4C2.EqualsWithDelta(other.mR4C2, delta) &&
                mR4C3.EqualsWithDelta(other.mR4C3, delta) &&
                mR4C4.EqualsWithDelta(other.mR4C4, delta);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Matrix other)
        {
            return Equals(other, Constants.DELTA);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Matrix)obj);
            
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return
                mR1C1.GetHashCode() ^ mR1C2.GetHashCode() ^ mR1C3.GetHashCode() ^ mR1C4.GetHashCode() ^
                mR2C1.GetHashCode() ^ mR2C2.GetHashCode() ^ mR2C3.GetHashCode() ^ mR2C4.GetHashCode() ^
                mR3C1.GetHashCode() ^ mR3C2.GetHashCode() ^ mR3C3.GetHashCode() ^ mR3C4.GetHashCode() ^
                mR4C1.GetHashCode() ^ mR4C2.GetHashCode() ^ mR4C3.GetHashCode() ^ mR4C4.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the matrix.
        /// </summary>
        /// <returns>
        /// The string representation of the matrix.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "R1C1: {0}, R1C2: {1}, R1C3: {2}, R1C4 {3}, " + Environment.NewLine +
                "R2C1: {4}, R2C2: {5}, R2C3: {6}, R2C4 {7}, " + Environment.NewLine +
                "R3C1: {8}, R3C2: {9}, R3C3: {10}, R3C4 {11}, " + Environment.NewLine +
                "R4C1: {12}, R4C2: {13}, R4C3: {14}, R4C4 {15}",
                mR1C1, mR1C2, mR1C3, mR1C4,
                mR2C1, mR2C2, mR2C3, mR2C4,
                mR3C1, mR3C2, mR3C3, mR3C4,
                mR4C1, mR4C2, mR4C3, mR4C4);
        }
    }
}