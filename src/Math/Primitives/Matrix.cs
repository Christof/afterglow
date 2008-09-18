namespace TheNewEngine.Math.Primitives
{
    /// <summary>
    /// 4x4 Matrix which can represent transformations like translation, scale, rotation.
    /// <remarks>
    /// The implementation is a row major matrix.
    /// </remarks>
    /// </summary>
    public struct Matrix
    {
        // Don't use auto properties because then the 
        // default StructLayout (which is LayoutKind.Sequential for structs) is not guaranteed.
        private Vector4 mFirstRow;

        private Vector4 mSecondRow;

        private Vector4 mThirdRow;

        private Vector4 mFourthRow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> struct.
        /// </summary>
        /// <param name="firstRow">The first row.</param>
        /// <param name="secondRow">The second row.</param>
        /// <param name="thirdRow">The third row.</param>
        /// <param name="fourthRow">The fourth row.</param>
        public Matrix(Vector4 firstRow, Vector4 secondRow, Vector4 thirdRow, Vector4 fourthRow)
        {
            mFirstRow = firstRow;
            mSecondRow = secondRow;
            mThirdRow = thirdRow;
            mFourthRow = fourthRow;
        }

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
            mFirstRow = new Vector4(r1c1, r1c2, r1c3, r1c4);
            mSecondRow = new Vector4(r2c1, r2c2, r2c3, r2c4);
            mThirdRow = new Vector4(r3c1, r3c2, r3c3, r3c4);
            mFourthRow = new Vector4(r4c1, r4c2, r4c3, r4c4);
        }

        /// <summary>
        /// Gets or sets the first row.
        /// </summary>
        /// <value>The first row.</value>
        public Vector4 FirstRow
        {
            get { return mFirstRow; }
            set { mFirstRow = value; }
        }

        /// <summary>
        /// Gets or sets the second row.
        /// </summary>
        /// <value>The second row.</value>
        public Vector4 SecondRow
        {
            get { return mSecondRow; }
            set { mSecondRow = value; }
        }

        /// <summary>
        /// Gets or sets the third row.
        /// </summary>
        /// <value>The third row.</value>
        public Vector4 ThirdRow
        {
            get { return mThirdRow; }
            set { mThirdRow = value; }
        }

        /// <summary>
        /// Gets or sets the fourth row.
        /// </summary>
        /// <value>The fourth row.</value>
        public Vector4 FourthRow
        {
            get { return mFourthRow; }
            set { mFourthRow = value; }
        }
    }
}