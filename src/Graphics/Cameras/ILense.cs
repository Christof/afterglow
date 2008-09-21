using TheNewEngine.Math.Primitives;

namespace TheNewEngine.Graphics.Cameras
{
    /// <summary>
    /// Defines a lense which is responsible to project a 3d scene to a 2d picture.
    /// </summary>
    public interface ILense
    {
        /// <summary>
        /// Gets or sets the distance to near the plane.
        /// </summary>
        /// <value>The distance to the near plane.</value>
        float DistanceToNearPlane { get; set; }

        /// <summary>
        /// Gets or sets the distance to the far plane.
        /// </summary>
        /// <value>The distance to the far plane.</value>
        float DistanceToFarPlane { get; set; }

        /// <summary>
        /// Gets or sets the horizontal field of view.
        /// </summary>
        /// <value>The horizontal field of view.</value>
        float HorizontalFieldOfView { get; set; }

        /// <summary>
        /// Gets or sets the aspect ratio.
        /// </summary>
        /// <value>The aspect ratio.</value>
        float AspectRatio { get; set; }

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        /// <value>The projection matrix.</value>
        Matrix ProjectionMatrix { get; }
    }
}