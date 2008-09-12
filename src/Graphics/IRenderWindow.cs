using System;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Interface for a window which can render object through an api implementation.
    /// </summary>
    public interface IRenderWindow : IDisposable, IScreenshotTaker
    {
        /// <summary>
        /// Renders the current scene.
        /// </summary>
        void Render();
    }
}