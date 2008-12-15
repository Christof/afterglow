using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Cameras
{
    /// <summary>
    /// The stand defines the position and orientation of the camera.
    /// </summary>
    public interface IStand
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        Vector3 Direction { get; set; }

        /// <summary>
        /// Gets or sets a vector point upwards.
        /// </summary>
        /// <value>The up vector.</value>
        Vector3 Up { get; set; }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        Matrix ViewMatrix { get; }
    }
}