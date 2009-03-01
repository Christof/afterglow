using System;

namespace Afterglow.Graphics
{
    /// <summary>
    /// Interface for a window which can render object through an api implementation.
    /// </summary>
    public interface IRenderWindow : IDisposable, IScreenshotTaker
    {
        /// <summary>
        /// Starts the rendering of the scene by cleaning the render target.
        /// </summary>
        void StartRendering();

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        void Render();
    }
}