using Afterglow.Graphics.Cameras;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Math;

namespace Afterglow.Graphics.Xna.Examples
{
    public class QuadWithIndexBuffer : SceneTestBase
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
            var indices = container.Create(GraphicStreamUsage.Index, CreateIndices());

            IBufferService bufferService = new XnaBufferService(RenderWindow.Device);

            var bufferBindings = new[]
            {
                bufferService.CreateFor(positions),
                bufferService.CreateFor(colors),
                bufferService.CreateFor(indices)
            };

            mEffect = new XnaEffectCompiler(RenderWindow.Device).Compile("MyShader.fx");

            mRenderer = new XnaObjectRenderer(RenderWindow.Device,
                mEffect, bufferBindings);

            mWorldViewProjectionParameter = new XnaMatrixEffectParameter("WorldViewProjection");
        }

        /// <summary>
        /// Updates the scene every frame.
        /// </summary>
        /// <param name="frameInfo">The frame info.</param>
        public override void Update(IFrameInfo frameInfo)
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
            var topLeft = new Vector3(-1f, 1f, 0f);
            var topRight = new Vector3(1f, 1f, 0f);
            var bottomLeft = new Vector3(-1f, -1f, 0f);
            var bottomRight = new Vector3(1f, -1f, 0f);

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private static Vector3[] CreateColors()
        {
            var topLeft = new Vector3(1f, 0f, 0f);
            var topRight = new Vector3(0f, 1f, 0f);
            var bottomLeft = new Vector3(0f, 0f, 1f);
            var bottomRight = new Vector3(1f, 1f, 0f);

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2 };
        }
    }
}