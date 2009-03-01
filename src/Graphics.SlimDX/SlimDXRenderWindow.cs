using System;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;

namespace Afterglow.Graphics
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

        private Texture2D mDepthBuffer;

        private DepthStencilView mDepthStencilView;

        private int mHeight;
 
        private int mWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXRenderWindow"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public SlimDXRenderWindow(IntPtr windowHandle)
        {
            mWindowHandle = windowHandle;

            try
            {
                // the debug mode requires the sdk to be installed otherwise an exception is thrown
                mDevice = new Device(DeviceCreationFlags.Debug);
            }
            catch (Direct3D10Exception)
            {
                mDevice = new Device(DeviceCreationFlags.None);
            }

            if (mIsWindowed)
            {
                mHeight = HEIGHT;
                mWidth = WIDTH;
            }
            else
            {
                mWidth = 1280;
                mHeight = 1024;
            }

            CreateSwapChainRenderTargetAndViewport();

            SetRasterizeStateDescription();
        }

        private void CreateSwapChainRenderTargetAndViewport()
        {
            var swapChainDescription = CreateSwapChainDescription();

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
                Width = mWidth,
                Height = mHeight,
                MinZ = 0.0f,
                MaxZ = 1.0f
            };

            CreateDepthBuffer();

            mDevice.Rasterizer.SetViewports(viewport);
            mDevice.OutputMerger.SetTargets(
                mDepthStencilView, mRenderTarget);
        }

        private ModeDescription CreateModeDescription()
        {
            return new ModeDescription
            {
                Format = Format.R8G8B8A8_UNorm,
                RefreshRate = new Rational(60, 1),
                Scaling = DisplayModeScaling.Unspecified,
                ScanlineOrdering = DisplayModeScanlineOrdering.Unspecified,
                Width = mWidth,
                Height = mHeight
            };
        }

        private SwapChainDescription CreateSwapChainDescription()
        {
            var sampleDescription = new SampleDescription { Count = 1, Quality = 0 };

            return new SwapChainDescription
            {
                ModeDescription = CreateModeDescription(),
                SampleDescription = sampleDescription,
                BufferCount = 1,
                Flags = SwapChainFlags.None,
                IsWindowed = mIsWindowed,
                OutputHandle = mWindowHandle,
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
        }

        private void CreateDepthBuffer()
        {
            // Create depth stencil texture
            var descDepth = new Texture2DDescription
            {
                Width = mWidth,
                Height = mHeight,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.D32_Float,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = 0,
                OptionFlags = ResourceOptionFlags.None
            };
            mDepthBuffer = new Texture2D(mDevice, descDepth);

            var descDSV = new DepthStencilViewDescription
            {
                Format = descDepth.Format,
                Dimension = DepthStencilViewDimension.Texture2D,
                MipSlice = 0
            };
            mDepthStencilView = new DepthStencilView(
                mDevice, mDepthBuffer, descDSV);
        }

        private void SetRasterizeStateDescription()
        {
            var rasterizerDesc = new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                DepthBias = 0,
                DepthBiasClamp = 0.0f,
                FillMode = FillMode.Solid,
                IsAntialiasedLineEnabled = false,
                IsDepthClipEnabled = true,
                IsFrontCounterclockwise = true,
                IsMultisampleEnabled = false,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = 0.0f
            };

            mDevice.Rasterizer.State = RasterizerState.FromDescription(
                mDevice, rasterizerDesc);
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
            mDevice.ClearDepthStencilView(mDepthStencilView,
                DepthStencilClearFlags.Depth, 1.0f, 0);
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
            mDepthBuffer.Dispose();
            mDepthStencilView.Dispose();

            mDevice.Dispose();
        }

        /// <summary>
        /// Switches betwenn fullscreen and windowed mode.
        /// </summary>
        public void SwitchFullscreen()
        {
            mSwapChain.Dispose();
            mRenderTarget.Dispose();
            mDepthBuffer.Dispose();
            mDepthStencilView.Dispose();

            mIsWindowed = !mIsWindowed;
            CreateSwapChainRenderTargetAndViewport();
        }
    }
}