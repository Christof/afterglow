using Afterglow.Graphics.Cameras;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Math;

namespace Afterglow.Graphics.Examples
{
    public class OpenTKQuadWithIndexBuffer : OpenTKSceneTestBase
    {
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

            IBufferService bufferService = new OpenTKBufferService();

            var bufferBindings = new[]
            {
                bufferService.CreateFor(positions),
             //   bufferService.CreateFor(colors),
                bufferService.CreateFor(indices)
            };

            mRenderer = new OpenTKObjectRenderer(bufferBindings);
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
            RenderWindow.StartRendering();

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