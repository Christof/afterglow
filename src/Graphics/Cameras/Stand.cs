using TheNewEngine.Math.Primitives;

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
        /// Gets or sets up.
        /// </summary>
        /// <value>Up.</value>
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
            direction.Normalize();
            up.Normalize();

            Vector3 s = direction.Cross(up);
            Vector3 u = s.Cross(direction);

            return new Matrix(
                s.X, s.Y, s.Z, position.X,
                u.X, u.Y, u.Z, position.Y,
                -direction.X, -direction.Y, -direction.Z, position.Z,
                0, 0, 0, 1);
        }
    }
}