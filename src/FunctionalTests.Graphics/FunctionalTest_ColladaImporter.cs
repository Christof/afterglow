using System.Linq;
using MbUnit.Framework;
using System.Windows.Forms;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Rendering;
using TheNewEngine.Math;
using System.Collections.Generic;

namespace TheNewEngine.Graphics
{
    public class FunctionalTest_ColladaImporter
    {
        private Form mForm;

        private SlimDXRenderWindow mRenderWindow;

        private const string COLLAD_PLANE = "suzanne.dae";

        [SetUp]
        public void Setup()
        {
            mForm = new Form
            {
                ClientSize = new System.Drawing.Size(800, 600)
            };

            mRenderWindow = new SlimDXRenderWindow(mForm.Handle);
        }

        [TearDown]
        public void TearDown()
        {
            mForm.Dispose();
        }

        private static IEnumerable<BufferBinding> CreateBufferBindings(
            IEnumerable<IGraphicStream> container,
            IBufferService bufferService)
        {
            var bindings = new List<BufferBinding>();

            foreach (var graphicStream in container)
            {
                switch (graphicStream.Description.Format)
                {
                    case GraphicStreamFormat.UInt:
                        bindings.Add(bufferService.CreateFor(
                            (GraphicStream<uint>)graphicStream));
                        break;

                    case GraphicStreamFormat.Vector2:
                        bindings.Add(bufferService.CreateFor(
                            (GraphicStream<Vector2>)graphicStream));
                        break;

                    case GraphicStreamFormat.Vector3:
                        bindings.Add(bufferService.CreateFor(
                            (GraphicStream<Vector3>)graphicStream));
                        break;

                    case GraphicStreamFormat.Vector4:
                        bindings.Add(bufferService.CreateFor(
                            (GraphicStream<Vector4>)graphicStream));
                        break;
                }
            }

            return bindings;
        }

        [Test]
        //[Category(Categories.EXAMPLES)]
        public void Run()
        {
            var importer = new ColladaImporter(COLLAD_PLANE);
            var container = importer.GetFirstMesh();

            IBufferService bufferService = new SlimDXBufferService(mRenderWindow.Device);

            var bufferBindings = CreateBufferBindings(container, bufferService);
            //bufferBindings = container.Select(stream => bufferService.CreateFor(stream));

            IEffect effect = new SlimDXEffectCompiler(mRenderWindow.Device)
                .Compile("NormalLighting10.fx");
            IObjectRenderer renderer = new SlimDXObjectRenderer(mRenderWindow, effect, bufferBindings);

            EffectParameter<Matrix> worldViewProjectionParameter =
                new SlimDXMatrixEffectParameter("WorldViewProjection");

            var stand = new OrbitingStand(5.0f, 0, 0);
            var cam = new Camera(stand, new PerspectiveProjectionLense());
//            cam.Stand.Position = new Vector3(0, 0, -3);
//            cam.Stand.Direction = -cam.Stand.Direction;

            // Create depth stencil texture
            var descDepth = new Texture2DDescription
            {
                Width = 800,
                Height = 600,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.D32_Float,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = 0,
                OptionFlags = ResourceOptionFlags.None
            };
            var depthBuffer = new Texture2D(
                mRenderWindow.Device, descDepth);

            // Create the depth stencil view
            var descDSV = new DepthStencilViewDescription();
            descDSV.Format = descDepth.Format;
            descDSV.Dimension = DepthStencilViewDimension.Texture2D;
            descDSV.MipSlice = 0;
            var depthStencil = new DepthStencilView(
                mRenderWindow.Device, depthBuffer, descDSV);

            mRenderWindow.Device.OutputMerger.SetTargets(
                depthStencil, mRenderWindow.RenderTarget);

            var rasterizerDesc = new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                DepthBias = 0,
                DepthBiasClamp = 0.0f,
                FillMode = FillMode.Solid,
                IsAntialiasedLineEnabled = true,
                IsDepthClipEnabled = true,
                IsFrontCounterclockwise = true,
                IsMultisampleEnabled = true,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = 0.0f
            };

            mRenderWindow.Device.Rasterizer.State = RasterizerState.FromDescription(
                mRenderWindow.Device, rasterizerDesc);


            mForm.KeyDown +=
                delegate(object sender, KeyEventArgs e)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.W:
                            stand.Radius -= 0.1f;
                            break;
                        case Keys.S:
                            stand.Radius += 0.1f;
                            break;
                        case Keys.A:
                            stand.Azimuth -= 0.1f;
                            break;
                        case Keys.D:
                            stand.Azimuth += 0.1f;
                            break;
                        case Keys.R:
                            stand.Declination += 0.1f;
                            break;
                        case Keys.F:
                            stand.Declination -= 0.1f;
                            break;
                        case Keys.Escape:
                            Application.Exit();
                            break;
                        case Keys.P:
                            mRenderWindow.TakeScreenshot("screenshot.bmp");
                            break;
                    }
                };

            Application.Idle +=
                delegate
                {
                    worldViewProjectionParameter.Value = cam.ViewProjectionMatrix;
                    worldViewProjectionParameter.SetParameterOn(effect);
                    
                    mRenderWindow.StartRendering();
                    mRenderWindow.Device.ClearDepthStencilView(depthStencil,
                        SlimDX.Direct3D10.DepthStencilClearFlags.Depth, 1.0f, 0);
                    
                    renderer.Render();
                    mRenderWindow.Render();

                    Application.DoEvents();
                };

            Application.Run(mForm);
        }
    }
}