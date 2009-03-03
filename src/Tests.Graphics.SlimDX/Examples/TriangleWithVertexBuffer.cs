using Afterglow.Graphics.Cameras;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Math;

namespace Afterglow.Graphics.SlimDX.Examples
{
    public class TriangleWithVertexBuffer : SceneTestBase
    {
        private IEffect mEffect;

        private SemanticEffectParameter<Matrix> mWorldViewProjectionParameter;

        private IObjectRenderer mRenderer;

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public override void Load()
        {
            var container = new GraphicStreamContainer();
            var positions = container.Create(GraphicStreamUsage.Position, CreatePositions());
            var colors = container.Create(GraphicStreamUsage.Color, CreateColors());

            IBufferService bufferService = new SlimDXBufferService(RenderWindow.Device);

            var bufferBindings = new[]
            {
                bufferService.CreateFor(positions),
                bufferService.CreateFor(colors)
            };

            mEffect = new SlimDXEffectCompiler(RenderWindow.Device).Compile("MyShader10.fx");

            mRenderer = new SlimDXObjectRenderer(RenderWindow.Device,
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

            RenderWindow.StartRendering();
            mWorldViewProjectionParameter.Value = worldViewProjection;
            mWorldViewProjectionParameter.SetParameterOn(mEffect);

            mRenderer.Render();

            RenderWindow.Render();
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