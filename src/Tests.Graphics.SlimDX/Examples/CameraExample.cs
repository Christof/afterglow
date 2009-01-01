using System.Windows.Forms;
using MbUnit.Framework;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Rendering;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class CameraExample
    {
        private Form mForm;

        private SlimDXRenderWindow mRenderWindow;

        [SetUp]
        public void Setup()
        {
            mForm = EmptyWindow.CreateForm();
            mRenderWindow = new SlimDXRenderWindow(mForm.Handle);
        }

        [TearDown]
        public void TearDown()
        {
            mForm.Dispose();
        }

        [Test]
        [Category(Categories.EXAMPLES)]
        public void Run()
        {
            var container = new GraphicStreamContainer();
            var positions = container.Create(GraphicStreamUsage.Position, CreatePositions());
            var colors = container.Create(GraphicStreamUsage.Color, CreateColors());

            IBufferService bufferService = new SlimDXBufferService(mRenderWindow.Device);

            var bufferBindings = new[]
            {
                bufferService.CreateFor(colors),
                bufferService.CreateFor(positions),
            };

            IEffect effect = new SlimDXEffectCompiler(mRenderWindow.Device).Compile("MyShader10.fx");
            IObjectRenderer renderer = new SlimDXObjectRenderer(mRenderWindow, effect, bufferBindings);

            EffectParameter<Matrix> worldViewProjectionParameter =
                new SlimDXMatrixEffectParameter("WorldViewProjection");

            var cam = new Camera(new Stand(), new PerspectiveProjectionLense());
            cam.Stand.Position = new Vector3(0, 0, 3);

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

        private static Vector3[] CreatePositions()
        {
            var top = new Vector3(0f, 1f, 0f);
            var left = new Vector3(-1f, -1f, 0f);
            var right = new Vector3(1f, -1f, 0f);

            return new[] { top, right, left };
        }

        private static Vector3[] CreateColors()
        {
            var top = new Vector3(1f, 0f, 0f);
            var left = new Vector3(0f, 1f, 0f);
            var right = new Vector3(0f, 0f, 1f);

            return new[] { top, right, left };
        }
    }
}