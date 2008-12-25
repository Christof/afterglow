using System.Windows.Forms;
using MbUnit.Framework;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using TheNewEngine.Graphics.SlimDX.GraphicStreams;
using TheNewEngine.Graphics.SlimDX.Effects;
using TheNewEngine.Graphics.SlimDX.Rendering;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class CameraExample
    {
        private Form mForm;

        private RenderWindow mRenderWindow;

        [SetUp]
        public void Setup()
        {
            mForm = EmptyWindow.CreateForm();
            mRenderWindow = new RenderWindow(mForm.Handle);
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
            container.Create(GraphicStreamUsage.Position, CreatePositions());
            container.Create(GraphicStreamUsage.Color, CreateColors());

            var containerImplementation = new BufferContainer(mRenderWindow.Device);
            container.Load(containerImplementation);

            IEffect effect = new EffectCompiler(mRenderWindow.Device).Compile("MyShader10.fx");
            IObjectRenderer renderer = new ObjectRenderer(mRenderWindow, effect, container);

            EffectParameter<Matrix> worldViewProjectionParameter =
                new MatrixEffectParameter("WorldViewProjection");

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