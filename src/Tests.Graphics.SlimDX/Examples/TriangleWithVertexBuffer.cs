using System.Windows.Forms;
using MbUnit.Framework;
using SlimDX;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using System.IO;
using TheNewEngine.Graphics.Utilities;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.SlimDX.Effects;
using TheNewEngine.Graphics.SlimDX.Rendering;

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

            var positionsBuffer = new SlimDXBuffer(mRenderWindow.Device);
            positionsBuffer.Load(positions);

            var colorsBuffer = new SlimDXBuffer(mRenderWindow.Device);
            colorsBuffer.Load(colors);
            
            var bufferBindings = new[]
            {
                new SlimDXBufferBinding(mRenderWindow.Device, positionsBuffer),
                new SlimDXBufferBinding(mRenderWindow.Device, colorsBuffer)
            };

            IEffect effect = new EffectCompiler(mRenderWindow.Device).Compile("MyShader10.fx");

            IObjectRenderer renderer = new ObjectRenderer(mRenderWindow,
                effect, bufferBindings);

            EffectParameter<Math.Matrix> worldViewProjectionParameter =
                new MatrixEffectParameter("WorldViewProjection");

            Application.Idle +=
                delegate
                {
                    Matrix view = Matrix.LookAtRH(new Vector3(0, 0, 3), new Vector3(), new Vector3(0, 1, 0));
                    Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                    Matrix world = Matrix.Identity;
                    Matrix worldViewProjection = world * view * projection;

                    mRenderWindow.StartRendering();

                    worldViewProjectionParameter.Value = worldViewProjection.ToMath();
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

        private static Math.Vector3[] CreatePositions()
        {
            var top = new Math.Vector3(0f, 1f, 0f);
            var left = new Math.Vector3(-1f, -1f, 0f);
            var right = new Math.Vector3(1f, -1f, 0f);

            return new[] { top, right, left };
        }

        private static Color4[] CreateColors()
        {
            var top = new Color4(1f, 0f, 0f);
            var left = new Color4(0f, 1f, 0f);
            var right = new Color4(0f, 0f, 1f);

            return new[] { top, right, left };
        }
    }
}