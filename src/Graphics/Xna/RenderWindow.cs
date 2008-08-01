using System;
using Microsoft.Xna.Framework.Graphics;
namespace TheNewEngine.Graphics.Xna
{
    /// <summary>
    /// Encapsulates the Xna-device so that it renders in the given window.
    /// </summary>
    public class RenderWindow : RenderWindowBase
    {
        private GraphicsDevice device;
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderWindow"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public RenderWindow(IntPtr windowHandle)
        {
            var presentationParameters = new PresentationParameters();
            presentationParameters.IsFullScreen = false;
            presentationParameters.BackBufferCount = 1;
            presentationParameters.BackBufferFormat = SurfaceFormat.Color;//SurfaceFormat.Rgba32;
            presentationParameters.BackBufferWidth = WIDTH;
            presentationParameters.BackBufferHeight = HEIGHT;
            presentationParameters.DeviceWindowHandle = IntPtr.Zero;
            presentationParameters.EnableAutoDepthStencil = true;
            presentationParameters.PresentOptions = PresentOptions.None;
            presentationParameters.SwapEffect = SwapEffect.Discard;
            presentationParameters.FullScreenRefreshRateInHz = 0;
            presentationParameters.MultiSampleQuality = 0;
            presentationParameters.MultiSampleType = MultiSampleType.None;
            presentationParameters.PresentationInterval = PresentInterval.Default;
            presentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;
            presentationParameters.AutoDepthStencilFormat = DepthFormat.Depth24;

            this.device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware,
                windowHandle, presentationParameters);
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public override void Render()
        {
            this.device.Clear(Color.Blue);

            this.device.Present();
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public override void TakeScreenshot(string filename)
        {
            using (var texture2D = new ResolveTexture2D(
                this.device, WIDTH, HEIGHT, 1, SurfaceFormat.Color))
            {
                this.device.ResolveBackBuffer(texture2D);

                texture2D.Save(filename, ImageFileFormat.Bmp);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            this.device.Dispose();
        }
    }
}