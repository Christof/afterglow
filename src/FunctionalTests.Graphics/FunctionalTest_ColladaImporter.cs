using System.Linq;
using MbUnit.Framework;
using System.Windows.Forms;
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

        private const string COLLAD_PLANE = "plane.dae";

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
                    renderer.Render();
                    mRenderWindow.Render();

                    Application.DoEvents();
                };

            Application.Run(mForm);
        }
    }
}