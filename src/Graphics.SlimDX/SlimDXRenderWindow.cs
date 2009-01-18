using System;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Encapsulates the SlimDX-device so that it renders in the given window.
    /// </summary>
    public class SlimDXRenderWindow : IRenderWindow
    {
        private const int HEIGHT = 600;

        private const int WIDTH = 800;

        private readonly Device mDevice;

        private readonly IntPtr mWindowHandle;

        private bool mIsWindowed = true;

        private RenderTargetView mRenderTarget;

        private SwapChain mSwapChain;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXRenderWindow"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public SlimDXRenderWindow(IntPtr windowHandle)
        {
            mWindowHandle = windowHandle;

            mDevice = new Device(DeviceCreationFlags.Debug);

            CreateSwapChainRenderTargetAndViewport(true);
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
            swapChainDescription.OutputHandle = mWindowHandle;
            swapChainDescription.SwapEffect = SwapEffect.Discard;
            swapChainDescription.Usage = Usage.RenderTargetOutput;

            using (var factory = new Factory())
            {
                mSwapChain = new SwapChain(factory, mDevice, swapChainDescription);
            }

            using (var resource = mSwapChain.GetBuffer<Texture2D>(0))
            {
                mRenderTarget = new RenderTargetView(mDevice, resource);
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

            mDevice.Rasterizer.SetViewports(viewport);
            mDevice.OutputMerger.SetTargets(mRenderTarget);
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <value>The device.</value>
        // TODO : change to internal
        public Device Device
        {
            get
            {
                return mDevice;
            }
        }

        /// <summary>
        /// Gets the render target.
        /// </summary>
        /// <value>The render target.</value>
        // TODO : change to internal
        public RenderTargetView RenderTarget
        {
            get
            {
                return mRenderTarget;
            }
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void TakeScreenshot(string filename)
        {
            var buffer = mSwapChain.GetBuffer<Texture2D>(0);

            Texture.ToFile(buffer, ImageFileFormat.Bmp, filename);
        }

        /// <summary>
        /// Starts the rendering of the scene by cleaning the render target.
        /// </summary>
        public void StartRendering()
        {
            mDevice.ClearRenderTargetView(mRenderTarget, new Color4(1, 0, 0));
        }

        /// <summary>
        /// Renders the current scene.
        /// </summary>
        public void Render()
        {
            mSwapChain.Present(0, PresentFlags.None);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            mSwapChain.Dispose();
            mRenderTarget.Dispose();

            mDevice.Dispose();
        }

        /// <summary>
        /// Switches betwenn fullscreen and windowed mode.
        /// </summary>
        public void SwitchFullscreen()
        {
            mSwapChain.Dispose();
            mRenderTarget.Dispose();

            mIsWindowed = !mIsWindowed;
            CreateSwapChainRenderTargetAndViewport(mIsWindowed);
        }
    }
}