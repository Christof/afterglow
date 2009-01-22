using System.Windows.Forms;
using MbUnit.Framework;
using TheNewEngine.Graphics.GraphicStreams;
using System.IO;
using TheNewEngine.Graphics.Rendering;
using TheNewEngine.Graphics.Utilities;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    public class TriangleWithVertexBuffer : SceneTestBase
    {
        private IEffect mEffect;

        private EffectParameter<Matrix> mWorldViewProjectionParameter;

        private IObjectRenderer mRenderer;

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public override void Load()
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

            mEffect = new SlimDXEffectCompiler(mRenderWindow.Device).Compile("MyShader10.fx");

            mRenderer = new SlimDXObjectRenderer(mRenderWindow,
                mEffect, bufferBindings);

            mWorldViewProjectionParameter = new SlimDXMatrixEffectParameter("WorldViewProjection");
        }

        /// <summary>
        /// Updates the scene every frame.
        /// </summary>
        /// <param name="timeSinceLastCall">The time since the last call.</param>
        public override void Update(float timeSinceLastCall)
        {
        }

        /// <summary>
        /// Renders the scene.
        /// </summary>
        public override void Render()
        {
            Matrix view = Stand.CalculateViewMatrix(
                new Vector3(0, 0, 3), -Vector3.ZAxis, Vector3.YAxis);
            Matrix projection = PerspectiveProjectionLense.CalculateProjectionMatrix(
                0.01f, 100f, (float)(System.Math.PI / 3), 800f / 600.0f);
            Matrix world = Matrix.Identity;
            Matrix worldViewProjection = world * view * projection;

            mRenderWindow.StartRendering();
            mWorldViewProjectionParameter.Value = worldViewProjection;
            mWorldViewProjectionParameter.SetParameterOn(mEffect);

            mRenderer.Render();

            mRenderWindow.Render();

            //AssertWithScreenshot();
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