namespace TheNewEngine.Graphics.Resources
{
    /// <summary>
    /// Decorator for a resource.
    /// </summary>
    public class ResourceDecorator : IResource
    {
        /// <summary>
        /// Gets the decoree.
        /// </summary>
        /// <value>The decoree.</value>
        public IResource Decoree { get; private set; }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        public virtual void Load(IResource decoree)
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