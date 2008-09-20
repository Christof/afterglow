namespace TheNewEngine.Math.Primitives
{
    /// <summary>
    /// 4x4 Matrix which can represent transformations like translation, scale, rotation.
    /// <remarks>
    /// The implementation is a row major matrix.
    /// </remarks>
    /// </summary>
    public struct Matrix : ICoordinateSystem
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
    }
}