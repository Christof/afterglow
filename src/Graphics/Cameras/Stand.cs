using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Cameras
{
    /// <summary>
    /// Matrix implementation for <see cref="IStand"/>.
    /// </summary>
    public class Stand : IStand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stand"/> class.
        /// </summary>
        public Stand()
        {
            Up = Vector3.YAxis;
            Direction = Vector3.ZAxis;
            Position = Vector3.Zero;
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Vector3 Direction { get; set; }

        /// <summary>
        /// Gets or sets a vector point upwards.
        /// </summary>
        /// <value>The up vector.</value>
        public Vector3 Up { get; set; }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        public Matrix ViewMatrix
        {
            get
            {
                return CalculateViewMatrix(Position, Direction, Up);
            }
        }

        /// <summary>
        /// Calculates the view matrix.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="up">The up direction.</param>
        /// <returns>The calculated view matrix.</returns>
        public static Matrix CalculateViewMatrix(Vector3 position, Vector3 direction, Vector3 up)
        {
            var normalizedDirection = direction.Normalized();

            var s = normalizedDirection.Cross(up.Normalized());
            var u = s.Cross(normalizedDirection);

            return new Matrix(
                s.X, s.Y, s.Z, 0,
                u.X, u.Y, u.Z, 0,
                -direction.X, -direction.Y, -direction.Z, 0,
                position.X, -position.Y, position.Z, 1);
        }
    }
}