using System;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;

namespace TheNewEngine.Graphics.SlimDX
{
    /// <summary>
    /// Encapsulates the SlimDX-device so that it renders in the given window.
    /// </summary>
    public class RenderWindow : RenderWindowBase
    {
        private readonly Device device;
        private SwapChain swapChain;
        private RenderTargetView renderTarget;
        private const int WIDTH = 800;
        private const int HEIGHT = 600;
        private IntPtr windowHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderWindow"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public RenderWindow(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;

            this.device = new Device(DeviceCreationFlags.Debug);

            this.CreateSwapChainRenderTargetAndViewport(true);
        }

        private void CreateSwapChainRenderTargetAndViewport(bool isWindowed)
        {
            var swapChainDescription = new SwapChainDescription();
            var modeDescription = new ModeDescription();
            var sampleDescription = new SampleDescription();

            modeDescription.Format = Format.R8G8B8A8_UNorm;
            modeDescription.RefreshRate = new Rational(60, 1);
            modeDescription.Scaling = DisplayModeScaling.Unspecified;
            modeDescription.ScanlineOrdering = DisplayModeScanlineOrdering.Unspecified;
            if (isWindowed)
            {
                modeDescription.Width = WIDTH;
                modeDescription.Height = HEIGHT;
            }
            else
            {
                modeDescription.Width = 1280;
                modeDescription.Height = 1024;
            }

            sampleDescription.Count = 1;
            sampleDescription.Quality = 0;

            swapChainDescription.ModeDescription = modeDescription;
            swapChainDescription.SampleDescription = sampleDescription;
            swapChainDescription.BufferCount = 1;
            swapChainDescription.Flags = SwapChainFlags.None;
            swapChainDescription.IsWindowed = isWindowed;
            swapChainDescription.OutputHandle = this.windowHandle;
            swapChainDescription.SwapEffect = SwapEffect.Discard;
            swapChainDescription.Usage = Usage.RenderTargetOutput;

            using (var factory = new Factory())
            {
                this.swapChain = new SwapChain(factory, this.device, swapChainDescription);
            }
            using (var resource = this.swapChain.GetBuffer<Texture2D>(0))
            {
                this.renderTarget = new RenderTargetView(this.device, resource);
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
            this.device.OutputMerger.SetTargets(this.renderTarget);
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public override void TakeScreenshot(string filename)
        {
            var buffer = this.swapChain.GetBuffer<Texture2D>(0);

            Texture.ToFile(buffer, ImageFileFormat.Bmp, filename);
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public override void Render()
        {
            this.device.ClearRenderTargetView(this.renderTarget, new Color4(1, 0, 0));

            this.swapChain.Present(0, PresentFlags.None);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            swapChain.Dispose();
            renderTarget.Dispose();

            device.Dispose();
        }

        private bool isWindowed = true;

        /// <summary>
        /// Switches betwenn fullscreen and windowed mode.
        /// </summary>
        public void SwitchFullscreen()
        {
            swapChain.Dispose();
            renderTarget.Dispose();

            isWindowed = !isWindowed;
            CreateSwapChainRenderTargetAndViewport(isWindowed);
        }
    }
}