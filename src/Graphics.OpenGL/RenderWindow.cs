using System;

namespace TheNewEngine.Graphics.OpenGL
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
        /// Starts the rendering of the scene by cleaning the render target.
        /// </summary>
        public void StartRendering()
        {
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public void Render()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void TakeScreenshot(string filename)
        {
        }
    }
}