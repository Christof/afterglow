namespace Afterglow.Graphics
{
    /// <summary>
    /// Contains information about the frame in relation to the last one.
    /// </summary>
    public struct FrameInfo : IFrameInfo
    {
        private readonly float mTimeSinceLastFrame;

        private readonly float mTimeSinceStart;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameInfo"/> struct.
        /// </summary>
        /// <param name="timeSinceLastFrame">The time since last frame.</param>
        /// <param name="timeSinceStart">The time since start.</param>
        public FrameInfo(float timeSinceLastFrame, float timeSinceStart)
        {
            mTimeSinceLastFrame = timeSinceLastFrame;
            mTimeSinceStart = timeSinceStart;
        }

        /// <summary>
        /// Gets the time since the last frame in seconds.
        /// </summary>
        /// <value>The time since last frame.</value>
        public float TimeSinceLastFrame
        {
            get { return mTimeSinceLastFrame; }
        }

        /// <summary>
        /// Gets the time since the start of the applicaton in seconds.
        /// </summary>
        /// <value>The time since start.</value>
        public float TimeSinceStart
        {
            get { return mTimeSinceStart; }
        }
    }
}