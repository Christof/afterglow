using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheNewEngine.Graphics.Xna
{
    /// <summary>
    /// Encapsulates the Xna-device so that it renders in the given window.
    /// </summary>
    public class RenderWindow : IRenderWindow
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        private GraphicsDevice mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderWindow"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public RenderWindow(IntPtr windowHandle)
        {
            var presentationParameters = new PresentationParameters
            {
                IsFullScreen = false,
                BackBufferCount = 1,
                BackBufferFormat = SurfaceFormat.Color,
                BackBufferWidth = WIDTH,
                BackBufferHeight = HEIGHT,
                DeviceWindowHandle = IntPtr.Zero,
                EnableAutoDepthStencil = true,
                PresentOptions = PresentOptions.None,
                SwapEffect = SwapEffect.Discard,
                FullScreenRefreshRateInHz = 0,
                MultiSampleQuality = 0,
                MultiSampleType = MultiSampleType.None,
                PresentationInterval = PresentInterval.Default,
                RenderTargetUsage = RenderTargetUsage.DiscardContents,
                AutoDepthStencilFormat = DepthFormat.Depth24
            };

            mDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware,
                windowHandle, presentationParameters);
        }

        /// <summary>
        /// Gets or sets the render action.
        /// </summary>
        /// <value>The render action.</value>
        public Action RenderAction
        {
            get;
            set;
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public void Render()
        {
            mDevice.Clear(Color.Blue);

            if (RenderAction != null)
            {
                RenderAction();
            }

            mDevice.Present();
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void TakeScreenshot(string filename)
        {
            using (var texture2D = new ResolveTexture2D(
                mDevice, WIDTH, HEIGHT, 1, SurfaceFormat.Color))
            {
                mDevice.ResolveBackBuffer(texture2D);

                texture2D.Save(filename, ImageFileFormat.Bmp);
            }
        }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>The device.</value>
        internal GraphicsDevice Device
        {
            get { return mDevice; }
            set { mDevice = value; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            mDevice.Dispose();
        }
    }
}