using System;
namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Base class for render windows.
    /// </summary>
    public abstract class RenderWindowBase : IDisposable, IScreenshotTaker
    {
        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public abstract void TakeScreenshot(string filename);
    }
}