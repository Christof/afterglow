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
    public class Test_ColladaImporter
    {
        private Form mForm;

        private SlimDXRenderWindow mRenderWindow;

        private const string COLLAD_PLANE = "sphere.dae";

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

        private IEnumerable<BufferBinding> CreateBufferBindings(
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

            IEffect effect = new SlimDXEffectCompiler(mRenderWindow.Device).Compile("MyShader10.fx");
            IObjectRenderer renderer = new SlimDXObjectRenderer(mRenderWindow, effect, bufferBindings);

            EffectParameter<Matrix> worldViewProjectionParameter =
                new SlimDXMatrixEffectParameter("WorldViewProjection");

            var cam = new Camera(new Stand(), new PerspectiveProjectionLense());
            cam.Stand.Position = new Vector3(0, 0, -3);
            cam.Stand.Direction = -cam.Stand.Direction;

            mForm.KeyDown +=
                delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.W)
                    {
                        cam.Stand.Position += cam.Stand.Direction * 0.1f;
                    }

                    if (e.KeyCode == Keys.S)
                    {
                        cam.Stand.Position -= cam.Stand.Direction * 0.1f;
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