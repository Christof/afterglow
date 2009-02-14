using System.Collections.Generic;
using System.Windows.Forms;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Rendering;
using TheNewEngine.Graphics.SlimDX;
using TheNewEngine.Input;
using TheNewEngine.Input.SlimDX;
using TheNewEngine.Math;
using TheNewEngine.Graphics.Textures;

namespace TheNewEngine.Graphics
{
    public class FunctionalTest : SceneTestBase
    {
        private const string COLLAD_PLANE = "plane.dae";

        private SemanticEffectParameter<Matrix> mWorldViewProjectionParameter;

        private SemanticEffectParameter<ITexture> mTextureParameter;

        private Camera mCamera;

        private OrbitingStand mStand;

        private IObjectRenderer mRenderer;

        private IEffect mEffect;

        private IInputDevices mInputDevices;

        private float mTimeSinceLastFrame;

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public override void Load()
        {
            mInputDevices = new SlimDXInputDevices(Form);

            var importer = new ColladaImporter(COLLAD_PLANE);
            var container = importer.GetFirstMesh();

            IBufferService bufferService = new SlimDXBufferService(RenderWindow.Device);

            var bufferBindings = CreateBufferBindings(container, bufferService);
            //bufferBindings = container.Select(stream => bufferService.CreateFor(stream));

            mEffect = new SlimDXEffectCompiler(RenderWindow.Device)
                //.Compile("NormalLighting10.fx");
                .Compile("MyTextureShader10.fx");
            mRenderer = new SlimDXObjectRenderer(RenderWindow, mEffect, bufferBindings);

            mWorldViewProjectionParameter =
                new SlimDXMatrixEffectParameter("WorldViewProjection");

            var texture = new SlimDXTexture("texture.png", RenderWindow.Device);
            texture.Load();

            mTextureParameter = new SlimDXTextureEffectParameter(
                RenderWindow.Device,
                "Texture", texture);

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
            mTimeSinceLastFrame = timeSinceLastCall;
            Form.Text = string.Format("FrameTime: {0:0000}ms FPS: {1:0000}", 
                timeSinceLastCall * 1000.0f, 1 / timeSinceLastCall);
            mInputDevices.Update();
        }

        /// <summary>
        /// Renders the scene.
        /// </summary>
        public override void Render()
        {
            mWorldViewProjectionParameter.Value = mCamera.ViewProjectionMatrix;
            mWorldViewProjectionParameter.SetParameterOn(mEffect);
            mTextureParameter.SetParameterOn(mEffect);

            RenderWindow.StartRendering();

            mRenderer.Render();

            RenderWindow.Render();
        }

        private void SetupKeysAndActions()
        {
            var keyboard = mInputDevices.Keyboard;
            keyboard.On(Input.Button.W).IsDown().Do(() => mStand.Radius -= mTimeSinceLastFrame);
            keyboard.On(Input.Button.S).IsDown().Do(() => mStand.Radius += mTimeSinceLastFrame);
            keyboard.On(Input.Button.A).IsDown().Do(() => mStand.Azimuth += mTimeSinceLastFrame);
            keyboard.On(Input.Button.D).IsDown().Do(() => mStand.Azimuth -= mTimeSinceLastFrame);
            keyboard.On(Input.Button.R).IsDown().Do(() => mStand.Declination += mTimeSinceLastFrame);
            keyboard.On(Input.Button.F).IsDown().Do(() => mStand.Declination -= mTimeSinceLastFrame);

            keyboard.On(Input.Button.Escape).WasPressed().Do(Application.Exit);
            keyboard.On(Input.Button.P).WasPressed().Do(() => RenderWindow.TakeScreenshot("screenshot.bmp"));
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