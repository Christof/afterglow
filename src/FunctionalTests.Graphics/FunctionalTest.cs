using System.Linq;
using System.Windows.Forms;
using Afterglow.Graphics.Cameras;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Graphics.Textures;
using Afterglow.Input;
using Afterglow.Math;
using Ninject;
using Ninject.Parameters;

namespace Afterglow.Graphics
{
    public class FunctionalTest : SceneTestBase
    {
        private const string COLLAD_PLANE = "plane.dae";

        private IRenderWindow mRenderWindow;

        private SemanticEffectParameter<Matrix> mWorldViewProjectionParameter;

        private SemanticEffectParameter<ITexture> mTextureParameter;

        private Camera mCamera;

        private OrbitingStand mStand;

        private IObjectRenderer mRenderer;

        private IEffect mEffect;

        private IInputDevices mInputDevices;

        private float mTimeSinceLastFrame;

        private IKernel mKernel;

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public override void Load()
        {
            mKernel = new StandardKernel(new XnaModule());

            var factory = mKernel.Get<IApiFactory>();
            mRenderWindow = factory.CreateRenderWindow(Form);

            mInputDevices = mKernel.Get<IInputDevices>(
                new ConstructorArgument("control", Form));

            var importer = new ColladaImporter(COLLAD_PLANE);
            var container = importer.GetFirstMesh();

            var bufferService = factory.GetBufferService();

            var bufferBindings = container
                .Select(stream => bufferService.CreateFor(stream))
                .ToArray(); // otherwise an interanl error of SlimDX occurs.

            mEffect = factory.GetEffectCompiler()
                //.Compile("NormalLighting10.fx");
                .Compile("MyTextureShader.fx");

            mRenderer = factory.CreateObjectRenderer(mEffect, bufferBindings);

            mWorldViewProjectionParameter = 
                factory.CreateMatrixParameter("WorldViewProjection");

            var texture = factory.CreateTexture("texture.png");
            texture.Load();

            mTextureParameter = factory.CreateTextureParameter("Texture", texture);

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

            mRenderWindow.StartRendering();

            mRenderer.Render();

            mRenderWindow.Render();
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

            keyboard.On(Input.Button.Escape).WasReleased().Do(Application.Exit);
            keyboard.On(Input.Button.P).WasReleased().Do(() => mRenderWindow.TakeScreenshot("screenshot.bmp"));
        }
    }
}