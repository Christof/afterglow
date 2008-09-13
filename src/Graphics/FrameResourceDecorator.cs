namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Decorator for a frame resource.
    /// </summary>
    public abstract class FrameResourceDecorator : IFrameResource
    {
        /// <summary>
        /// Gets the decoree.
        /// </summary>
        /// <value>The decoree.</value>
        public IFrameResource Decoree { get; private set; }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        public virtual void Load(IFrameResource decoree)
        {
            Decoree = decoree;

            Decoree.Load(this);
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public virtual void Unload()
        {
            Decoree.Unload();
        }

        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public virtual void OnFrame()
        {
            Decoree.OnFrame();
        }
    }
}