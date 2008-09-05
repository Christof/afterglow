namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Abstract implementation of a resource which can be loaded and unloaded.
    /// </summary>
    public abstract class LoadableResource
    {
        /// <summary>
        /// Loads the resource.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public abstract void Unload();
    }
}