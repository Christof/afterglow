using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;

namespace Afterglow.Graphics.SlimDX.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class EmptyWindow
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 600;

        [Test]
        public void Run()
        {
            using (var form = CreateForm())
            {
                Device device;
                SwapChain swapChain;
                RenderTargetView renderTarget;

                CreateDeviceSwapChainAndRenderTarget(form, out device, out swapChain, out renderTarget);

                Application.Idle +=
                    delegate
                    {
                        device.ClearRenderTargetView(renderTarget, new Color4(1, 0, 0));

                        swapChain.Present(0, PresentFlags.None);

                        Application.DoEvents();
                    };

                Application.Run(form);
            }   
        }

        public static void CreateDeviceSwapChainAndRenderTarget(Form form,
            out Device device, out SwapChain swapChain, out RenderTargetView renderTarget)
        {
            try
            {
                // the debug mode requires the sdk to be installed otherwise an exception is thrown
                device = new Device(DeviceCreationFlags.Debug);
            }
            catch (Direct3D10Exception)
            {
                device = new Device(DeviceCreationFlags.None);
            }

            var swapChainDescription = new SwapChainDescription();
            var modeDescription = new ModeDescription();
            var sampleDescription = new SampleDescription();

            modeDescription.Format = Format.R8G8B8A8_UNorm;
            modeDescription.RefreshRate = new Rational(60, 1);
            modeDescription.Scaling = DisplayModeScaling.Unspecified;
            modeDescription.ScanlineOrdering = DisplayModeScanlineOrdering.Unspecified;

            modeDescription.Width = WIDTH;
            modeDescription.Height = HEIGHT;

            sampleDescription.Count = 1;
            sampleDescription.Quality = 0;

            swapChainDescription.ModeDescription = modeDescription;
            swapChainDescription.SampleDescription = sampleDescription;
            swapChainDescription.BufferCount = 1;
            swapChainDescription.Flags = SwapChainFlags.None;
            swapChainDescription.IsWindowed = true;
            swapChainDescription.OutputHandle = form.Handle;
            swapChainDescription.SwapEffect = SwapEffect.Discard;
            swapChainDescription.Usage = Usage.RenderTargetOutput;

            using (var factory = new Factory())
            {
                swapChain = new SwapChain(factory, device, swapChainDescription);
            }

            using (var resource = swapChain.GetBuffer<Texture2D>(0))
            {
                renderTarget = new RenderTargetView(device, resource);
            }

            var viewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width = WIDTH,
                Height = HEIGHT,
                MinZ = 0.0f,
                MaxZ = 1.0f
            };

            device.Rasterizer.SetViewports(viewport);
            device.OutputMerger.SetTargets(renderTarget);
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
    }
}