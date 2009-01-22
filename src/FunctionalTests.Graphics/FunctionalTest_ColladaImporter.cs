using System.Collections.Generic;
using System.Windows.Forms;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Rendering;
using TheNewEngine.Graphics.SlimDX;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics
{
    public class FunctionalTest_ColladaImporter : SceneTestBase
    {
        private const string COLLAD_PLANE = "thing.dae";

        private EffectParameter<Matrix> mWorldViewProjectionParameter;

        private Camera mCamera;

        private OrbitingStand mStand;

        private IObjectRenderer mRenderer;

        private IEffect mEffect;

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public override void Load()
        {
            var importer = new ColladaImporter(COLLAD_PLANE);
            var container = importer.GetFirstMesh();

            IBufferService bufferService = new SlimDXBufferService(mRenderWindow.Device);

            var bufferBindings = CreateBufferBindings(container, bufferService);
            //bufferBindings = container.Select(stream => bufferService.CreateFor(stream));

            mEffect = new SlimDXEffectCompiler(mRenderWindow.Device)
                .Compile("NormalLighting10.fx");
            mRenderer = new SlimDXObjectRenderer(mRenderWindow, mEffect, bufferBindings);

            mWorldViewProjectionParameter =
                new SlimDXMatrixEffectParameter("WorldViewProjection");

            mStand = new OrbitingStand(5.0f, 0, 0);
            mCamera = new Camera(mStand, new PerspectiveProjectionLense());

            SetupKeysAndActions();
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
            mWorldViewProjectionParameter.Value = mCamera.ViewProjectionMatrix;
            mWorldViewProjectionParameter.SetParameterOn(mEffect);

            mRenderWindow.StartRendering();

            mRenderer.Render();

            mRenderWindow.Render();
        }

        private void SetupKeysAndActions()
        {
            mForm.KeyDown +=
                delegate(object sender, KeyEventArgs e)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.W:
                            mStand.Radius -= 0.1f;
                            break;
                        case Keys.S:
                            mStand.Radius += 0.1f;
                            break;
                        case Keys.A:
                            mStand.Azimuth -= 0.1f;
                            break;
                        case Keys.D:
                            mStand.Azimuth += 0.1f;
                            break;
                        case Keys.R:
                            mStand.Declination += 0.1f;
                            break;
                        case Keys.F:
                            mStand.Declination -= 0.1f;
                            break;
                        case Keys.Escape:
                            Application.Exit();
                            break;
                        case Keys.P:
                            mRenderWindow.TakeScreenshot("screenshot.bmp");
                            break;
                    }
                };
        }

        private static IEnumerable<BufferBinding> CreateBufferBindings(
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
    }
}