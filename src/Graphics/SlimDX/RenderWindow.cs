using System;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device=SlimDX.Direct3D10.Device;

namespace TheNewEngine.Graphics.SlimDX
{
    public class RenderWindow : IDisposable
    {
        private Device device;
        private SwapChain swapChain;
        private RenderTargetView renderTarget;
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        public RenderWindow(IntPtr windowHandle)
        {
            this.device = new Device(DeviceCreationFlags.Debug);

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
            swapChainDescription.OutputHandle = windowHandle;
            swapChainDescription.SwapEffect = SwapEffect.Discard;
            swapChainDescription.Usage = Usage.RenderTargetOutput;

            using (var factory = new Factory())
            {
                this.swapChain = new SwapChain(factory, this.device, swapChainDescription);
            }
            using (var resource = swapChain.GetBuffer<Texture2D>(0))
            {
                this.renderTarget = new RenderTargetView(device, resource);
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

            this.device.Rasterizer.SetViewports(viewport);
            this.device.OutputMerger.SetTargets(renderTarget);
        }

        public void TakeScreenshot(string filename)
        {
            Texture2D buffer = this.swapChain.GetBuffer<Texture2D>(0);

            Texture.ToFile(buffer, ImageFileFormat.Png, filename);
        }

        public void Render()
        {
            this.device.ClearRenderTargetView(this.renderTarget, new Color4(1, 0, 0));

            this.swapChain.Present(0, PresentFlags.None);
        }

        public void Dispose()
        {
            this.swapChain.Dispose();
            this.renderTarget.Dispose();

            this.device.Dispose();
        }
    }
}