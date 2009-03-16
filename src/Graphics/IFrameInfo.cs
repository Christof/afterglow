namespace Afterglow.Graphics
{
    /// <summary>
    /// Contains inforamtion about a frame in relation to the last frame.
    /// </summary>
    public interface IFrameInfo
    {
        /// <summary>
        /// Gets the time since the last frame in seconds.
        /// </summary>
        /// <value>The time since last frame.</value>
        float TimeSinceLastFrame { get; }

        /// <summary>
        /// Gets the time since the start of the applicaton in seconds.
        /// </summary>
        /// <value>The time since start.</value>
        float TimeSinceStart { get; }
    }
}