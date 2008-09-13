using System;

namespace TheNewEngine.Graphics.OpenGl
{
    /// <summary>
    /// Encapsulates OpenGl so that it renders in the given window.
    /// </summary>
    public class RenderWindow : IRenderWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderWindow"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public RenderWindow(IntPtr windowHandle)
        {
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public void Render()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void TakeScreenshot(string filename)
        {
            throw new System.NotImplementedException();
        }
    }
}