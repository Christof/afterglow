namespace TheNewEngine.Graphics.Resources
{
    /// <summary>
    /// Encapsulation for a Resource which needs to be loaded to use it.
    /// Each frame it can be updated. If it isn't used any more it can be unloaded.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Called each frame before rendering so the the resource could be updated.
        /// </summary>
        void OnFrame();

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        void Load(IResource decoree);

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        void Unload();
    }
}