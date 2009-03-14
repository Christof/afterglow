namespace Afterglow.Math
{
    /// <summary>
    /// Represents a rotation in 3d-space with four values (a rotation axis and an angle).
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Quaternion
    /// </remarks>
    ///</summary>
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
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="rotationAxis">The rotation axis.</param>
        /// <param name="angle">The angle.</param>
        public Quaternion(Vector3 rotationAxis, float angle)
        {
            var s = Functions.Sqrt(2 * (1 + Functions.Cos(angle)));

            mValues = new Vector4(rotationAxis / s, s / 2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="vector3">The vector which should be represented as quaternion.</param>
        public Quaternion(Vector3 vector3)
        {
            mValues = new Vector4(vector3, 0);
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

        /// <summary>
        /// Returns the axis angle representation in a <see cref="Vector4"/>. The
        /// x, y and z componentes are the axis and w is the angle.
        /// </summary>
        /// <returns></returns>
        public Vector4 ToAxisAngle()
        {
            var angle = 2 * Functions.Acos(mValues.W);
            var length = Functions.Sqrt(1 - mValues.W * mValues.W);

            return new Vector4(mValues.ToVector3() / length, angle);
        }

        /// <summary>
        /// Converts to quaternion to a rotation matrix.
        /// </summary>
        /// <returns>The rotation matrix.</returns>
        public Matrix ToMatrix()
        {
            var m1 = new Matrix(
                 W, -Z,  Y,  X,
                 Z,  W, -X,  Y,
                -Y,  X,  W,  Z,
                -X, -Y, -Z,  W);
            var m2 = new Matrix(
                 W, -Z,  Y, -X,
                 Z,  W, -X, -Y,
                -Y,  X,  W, -Z,
                 X,  Y,  Z,  W);

            return m1 * m2;
        }

        /// <summary>
        /// Concatenates two quaternions. The right argument is applied first.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>A quaternion containing the rotations of both given quaternions.</returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            var v1 = new Vector3(right.X, right.Y, right.Z);
            var v2 = new Vector3(left.X, left.Y, left.Z);

            var w = left.W * right.W - v1.Dot(v2);
            var v = right.W * v2 + left.W * v1 + v2.Cross(v1);

            return new Quaternion(v, w);
        }
    }
}