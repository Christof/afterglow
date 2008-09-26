using TheNewEngine.Math.Primitives;

namespace TheNewEngine.Graphics.Cameras
{
    /// <summary>
    /// Default camera implementation.
    /// </summary>
    public class Camera : ICamera
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="stand">The stand which contains the view matrix.</param>
        /// <param name="lense">The lense which contains the projection matrix.</param>
        public Camera(IStand stand, ILense lense)
        {
            Stand = stand;
            Lense = lense;
        }

        /// <summary>
        /// Gets or sets the stand.
        /// </summary>
        /// <value>The stand.</value>
        public IStand Stand { get; set; }

        /// <summary>
        /// Gets or sets the lense.
        /// </summary>
        /// <value>The lense.</value>
        public ILense Lense { get; set; }

        /// <summary>
        /// Gets the view projection matrix.
        /// </summary>
        /// <value>The view projection matrix.</value>
        public Matrix ViewProjectionMatrix
        {
            get
            {
                return Stand.ViewMatrix * Lense.ProjectionMatrix;
            }
        }
    }
}