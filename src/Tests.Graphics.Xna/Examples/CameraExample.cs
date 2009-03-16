using System.Windows.Forms;
using Afterglow.Graphics.Cameras;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Math;

namespace Afterglow.Graphics.Xna.Examples
{
    public class CameraExample : SceneTestBase
    {
        private IObjectRenderer mRenderer;

        private SemanticEffectParameter<Matrix> mWorldViewProjectionParameter;

        private Camera mCamera;

        private IEffect mEffect;

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public override void Load()
        {
            var container = new GraphicStreamContainer();
            var positions = container.Create(GraphicStreamUsage.Position, CreatePositions());
            var colors = container.Create(GraphicStreamUsage.Color, CreateColors());
            
            IBufferService bufferService = new XnaBufferService(RenderWindow.Device);

            var bufferBindings = new[]
            {
                bufferService.CreateFor(colors),
                bufferService.CreateFor(positions),
            };

            mEffect = new XnaEffectCompiler(RenderWindow.Device).Compile("MyShader.fx");
            mRenderer = new XnaObjectRenderer(RenderWindow.Device, mEffect, bufferBindings);

            mWorldViewProjectionParameter = new XnaMatrixEffectParameter("WorldViewProjection");

            mCamera = new Camera(new Stand(), new PerspectiveProjectionLense());
            mCamera.Stand.Position = new Vector3(0, 0, 3);

            SetupKeysAndActions();
        }

        private void SetupKeysAndActions()
        {
            Form.KeyDown +=
                delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.W)
                    {
                        mCamera.Stand.Position += mCamera.Stand.Direction * 0.1f;
                    }

                    if (e.KeyCode == Keys.S)
                    {
                        mCamera.Stand.Position -= mCamera.Stand.Direction * 0.1f;
                    }
                };
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
            mWorldViewProjectionParameter.Value = mCamera.ViewProjectionMatrix;
            mWorldViewProjectionParameter.SetParameterOn(mEffect);

            RenderWindow.StartRendering();
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