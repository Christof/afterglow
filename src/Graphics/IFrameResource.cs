namespace TheNewEngine.Graphics
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
        void Load();

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        void Unload();
    }
}