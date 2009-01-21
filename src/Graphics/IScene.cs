namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Interface for a scene.
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        void Load();

        /// <summary>
        /// Updates the scene every frame.
        /// </summary>
        void Update();
    }
}