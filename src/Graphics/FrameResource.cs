namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Abstract implementation for a resource which must be manipulated each frame.
    /// </summary>
    public abstract class FrameResource : LoadableResource, IFrameResource
    {
        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public abstract void OnFrame();
    }
}