using System;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Afterglow.Graphics.Xna.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class EmptyWindow
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        [Test]
        public void Run()
        {
            using (var form = CreateForm())
            {
                PresentationParameters presentationParameters = CreatePresentationParameters();

                var device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware,
                    form.Handle, presentationParameters);

                Application.Idle +=
                    delegate
                    {
                        device.Clear(Color.Blue);

                        device.Present();

                        Application.DoEvents();
                    };

                Application.Run(form);
            }
        }

        /// <summary>
        /// Creates a form with a set client size.
        /// </summary>
        /// <returns>The new form.</returns>
        public static Form CreateForm()
        {
            return new Form
            {
                ClientSize = new System.Drawing.Size(WIDTH, HEIGHT)
            };
        }

        /// <summary>
        /// Creates presentation parameters with default values.
        /// </summary>
        /// <returns>The presentation parameters.</returns>
        public static PresentationParameters CreatePresentationParameters()
        {
            return new PresentationParameters
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
        }
    }
}