namespace TheNewEngine.Graphics.Resources
{
    /// <summary>
    /// Resource which needs an update every frame.
    /// </summary>
    public interface IFrameResource
    {
        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        void OnFrame();

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        void Load(IFrameResource decoree);

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        void Unload();
    }
}