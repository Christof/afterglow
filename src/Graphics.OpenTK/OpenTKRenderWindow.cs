using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace Afterglow.Graphics
{
    /// <summary>
    /// Encapsulates the OpenTK-device so that it renders in the given window.
    /// </summary>
    public class OpenTKRenderWindow : IRenderWindow
    {
        private const int HEIGHT = 600;

        private const int WIDTH = 800;

        private readonly GLControl mGLControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKRenderWindow"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public OpenTKRenderWindow(Control control)
        {
            mGLControl = new GLControl
            {
                Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right))),
                BackColor = Color.LightGreen,
                Location = new Point(1, 0),
                Name = "GL Control",
                Size = new Size(WIDTH, HEIGHT),
                TabIndex = 0,
                VSync = false
            };
            mGLControl.Resize += glControl_Resize;

            control.Controls.Add(mGLControl);
        }

        /// <summary>
        /// Gets or sets the render action.
        /// </summary>
        /// <value>The render action.</value>
        public Action RenderAction { get; set; }

        #region IRenderWindow Members

        /// <summary>
        /// Starts the rendering of the scene by cleaning the render target.
        /// </summary>
        public void StartRendering()
        {
            mGLControl.MakeCurrent();

            GL.ClearColor(mGLControl.BackColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public void Render()
        {
            if (RenderAction != null)
            {
                RenderAction();
            }

            mGLControl.SwapBuffers();
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void TakeScreenshot(string filename)
        {
            Bitmap bitmap = mGLControl.GrabScreenshot();
            bitmap.Save(filename);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!mGLControl.Disposing)
            {
                mGLControl.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// Handler for a resize event of the gl control
        /// </summary>
        /// <param name="sender">sender of the event, should be the parent control</param>
        /// <param name="e">arguments for the event</param>
        private void glControl_Resize(object sender, EventArgs e)
        {
            if (mGLControl.ClientSize.Height == 0)
            {
                mGLControl.ClientSize = new Size(mGLControl.ClientSize.Width, 1);
            }

            GL.Viewport(0, 0, mGLControl.ClientSize.Width, mGLControl.ClientSize.Height);
        }
    }
}