using System.Drawing;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.Direct3D10;
using SlimDX.DirectWrite;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;
using Font = SlimDX.Direct3D10.Font;
using DirectWriteFactory = SlimDX.DirectWrite.Factory;
using Direct2DFactory = SlimDX.Direct2D.Factory;
using FontWeight = SlimDX.DirectWrite.FontWeight;
using FontStyle = SlimDX.DirectWrite.FontStyle;

namespace Afterglow.Graphics.SlimDX.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class Text
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        private Form mForm;

        [SetUp]
        public void Setup()
        {
            mForm = EmptyWindow.CreateForm();
        }

        [TearDown]
        public void Teardown()
        {
            mForm.Dispose();
        }

        [Test]
        public void Run()
        {
            Device device;
            SwapChain swapChain;
            RenderTargetView renderTarget;

            EmptyWindow.CreateDeviceSwapChainAndRenderTarget(mForm, out device, out swapChain, out renderTarget);

            // buffer size 1 .. 4096 (0 defaults to 4096)
            var sprite = new Sprite(device, 10);
            var font = new Font(device, 20, "ARIAL");

            Application.Idle +=
                delegate
                {
                    device.ClearRenderTargetView(renderTarget, new Color4(1, 0, 0));
                    
                    var rectangle = new Rectangle(10, 10, 400, 300);
                    sprite.Begin(SpriteFlags.None);
                    font.Draw(sprite, "Hello from SlimDX", rectangle, FontDrawFlags.Left,
                        (uint)Color.Yellow.ToArgb());
                    sprite.End();

                    swapChain.Present(0, PresentFlags.None);

                    Application.DoEvents();
                };

            Application.Run(mForm);  
        }

        [Test]
        public void Using_DirectWrite()
        {
            Device device;
            SwapChain swapChain;
            RenderTargetView renderTarget;

            EmptyWindow.CreateDeviceSwapChainAndRenderTarget(mForm, out device, out swapChain, out renderTarget);

            var direct2dfactory = new Direct2DFactory();
            var directWriteFactory = new DirectWriteFactory();

            var properties = new WindowRenderTargetProperties { Handle = mForm.Handle, PixelSize = mForm.ClientSize };
            var windowRenderTarget = new WindowRenderTarget(direct2dfactory, properties);
            var brush = new SolidColorBrush(windowRenderTarget, Color.Black);

            var textFormat = new TextFormat(directWriteFactory, "Gabriola", 
                FontWeight.Normal, FontStyle.Normal, FontStretch.Normal, 72.0f, "en-us")
            {
                TextAlignment = TextAlignment.Center,
                ParagraphAlignment = ParagraphAlignment.Center
            };

            Application.Idle +=
                delegate
                {
                    device.ClearRenderTargetView(renderTarget, new Color4(1, 0, 0));

                    if (!windowRenderTarget.IsOccluded)
                    {
                        windowRenderTarget.BeginDraw();
                        windowRenderTarget.Transform = Matrix3x2.Identity;
                        windowRenderTarget.Clear(Color.White);

                        windowRenderTarget.DrawText("Hello World using DirectWrite!", textFormat, mForm.ClientRectangle, brush);

                        windowRenderTarget.EndDraw();
                    }

                    swapChain.Present(0, PresentFlags.None);

                    Application.DoEvents();
                };

            Application.Run(mForm);
        }
    }
}