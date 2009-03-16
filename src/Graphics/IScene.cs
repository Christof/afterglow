namespace Afterglow.Graphics
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
        /// <param name="frameInfo">The frame info.</param>
        void Update(IFrameInfo frameInfo);

        /// <summary>
        /// Renders the scene.
        /// </summary>
        void Render();
    }
}