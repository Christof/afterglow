using TheNewEngine.Math.Primitives;
using TheNewEngine.Math;
namespace TheNewEngine.Graphics.Cameras
{
    /// <summary>
    /// Implements the <see cref="ILense"/>-interface by converting
    /// the 3d scene to a 2d scene with a perspective projection so
    /// that objects farer away appear smaller than near ones.
    /// </summary>
    public class PerspectiveProjectionLense : ILense
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerspectiveProjectionLense"/> class.
        /// </summary>
        public PerspectiveProjectionLense()
        {
            AspectRatio = 4.0f / 3.0f;
            HorizontalFieldOfView = 0.75f;
            DistanceToFarPlane = 100000f;
            DistanceToNearPlane = 0.001f;
        }

        /// <summary>
        /// Gets or sets the distance to near the plane.
        /// </summary>
        /// <value>The distance to the near plane.</value>
        public float DistanceToNearPlane { get; set; }

        /// <summary>
        /// Gets or sets the distance to the far plane.
        /// </summary>
        /// <value>The distance to the far plane.</value>
        public float DistanceToFarPlane { get; set; }

        /// <summary>
        /// Gets or sets the horizontal field of view.
        /// </summary>
        /// <value>The horizontal field of view.</value>
        public float HorizontalFieldOfView { get; set; }

        /// <summary>
        /// Gets or sets the aspect ratio.
        /// </summary>
        /// <value>The aspect ratio.</value>
        public float AspectRatio { get; set; }

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        /// <value>The projection matrix.</value>
        public Matrix ProjectionMatrix
        {
            get
            {
                return CalculateProjectionMatrix(
                    DistanceToNearPlane, DistanceToFarPlane,
                    HorizontalFieldOfView, AspectRatio);
            }
        }

        /// <summary>
        /// Calculates the projection matrix.
        /// </summary>
        /// <param name="distanceToNearPlane">The distance to near plane.</param>
        /// <param name="distanceToFarPlane">The distance to far plane.</param>
        /// <param name="horizontalFieldOfView">The horizontal field of view.</param>
        /// <param name="aspectRatio">The aspect ratio.</param>
        /// <returns>The projection matrix.</returns>
        public static Matrix CalculateProjectionMatrix(float distanceToNearPlane, float distanceToFarPlane,
            float horizontalFieldOfView, float aspectRatio)
        {
            var verticalFieldOfView = horizontalFieldOfView / aspectRatio;
            var f = Functions.CoTan(verticalFieldOfView / 2);
            var dp = distanceToFarPlane - distanceToNearPlane;

            var projection = Matrix.Identity;
            projection.R1C1 = f / aspectRatio;
            projection.R2C2 = f;
            projection.R3C3 = -(distanceToFarPlane + distanceToNearPlane) / dp;
            projection.R3C4 = -1.0f;
            projection.R4C3 = -distanceToFarPlane * distanceToNearPlane / dp;
            projection.R4C4 = 0;

            return projection;
        }
    }
}