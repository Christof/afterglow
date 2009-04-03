using System.Drawing;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;
using Font = SlimDX.Direct3D10.Font;

namespace Afterglow.Graphics.SlimDX.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class Text
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        [Test]
        public void Run()
        {
            using (var form = EmptyWindow.CreateForm())
            {
                Device device;
                SwapChain swapChain;
                RenderTargetView renderTarget;

                EmptyWindow.CreateDeviceSwapChainAndRenderTarget(form, out device, out swapChain, out renderTarget);

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

                Application.Run(form);
            }   
        }
    }
}