using System.Windows.Forms;
using MbUnit.Framework;
using TheNewEngine.Graphics.GraphicStreams;
using System.IO;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using TheNewEngine.Graphics.Utilities;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.SlimDX.Effects;
using TheNewEngine.Graphics.SlimDX.Rendering;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class TriangleWithVertexBuffer
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
            var positions = container.Create(GraphicStreamUsage.Position, CreatePositions());
            var colors = container.Create(GraphicStreamUsage.Color, CreateColors());

            IBufferService bufferService = new SlimDXBufferService(mRenderWindow.Device);
            
            var bufferBindings = new[]
            {
                bufferService.CreateFor(positions),
                bufferService.CreateFor(colors)
            };

            IEffect effect = new EffectCompiler(mRenderWindow.Device).Compile("MyShader10.fx");

            IObjectRenderer renderer = new ObjectRenderer(mRenderWindow,
                effect, bufferBindings);

            EffectParameter<Matrix> worldViewProjectionParameter =
                new MatrixEffectParameter("WorldViewProjection");

            Application.Idle +=
                delegate
                {
                    Matrix view = Stand.CalculateViewMatrix(
                        new Vector3(0, 0, 3), -Vector3.ZAxis, Vector3.YAxis);
                    Matrix projection = PerspectiveProjectionLense.CalculateProjectionMatrix(
                        0.01f, 100f, (float)(System.Math.PI / 3), 800f / 600.0f);
                    Matrix world = Matrix.Identity;
                    Matrix worldViewProjection = world * view * projection;

                    mRenderWindow.StartRendering();

                    worldViewProjectionParameter.Value = worldViewProjection;
                    worldViewProjectionParameter.SetParameterOn(effect);

                    renderer.Render();

                    mRenderWindow.Render();

                    //AssertWithScreenshot();

                    Application.DoEvents();
                };

            Application.Run(mForm);
        }

        private void AssertWithScreenshot()
        {
            var name = "TheNewEngine.Graphics.SlimDX.Examples.TriangleWithVertexBuffer.Run";
            var expected = name + "_expected.bmp";
            var actual = name + "_actual.bmp";
            if (File.Exists(expected))
            {
                mRenderWindow.TakeScreenshot(actual);

                Assert.IsTrue(ImageComparer.Compare(expected, actual));
            }
            else
            {
                mRenderWindow.TakeScreenshot(expected);
                Assert.Fail("First run. Excpected image was taken and must be verified");
            }

            Application.Exit();
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