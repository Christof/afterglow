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
            mRenderWindow = new XnaRenderWindow(Form.Handle);
            mKernel = new StandardKernel(new XnaModule(mRenderWindow));

            mInputDevices = mKernel.Get<IInputDevices>(
                new ConstructorArgument("control", Form));

            var importer = new ColladaImporter(COLLAD_PLANE);
            var container = importer.GetFirstMesh();

            var bufferService = mKernel.Get<IBufferService>();

            var bufferBindings = container
                .Select(stream => bufferService.CreateFor(stream))
                .ToArray(); // otherwise an interanl error of SlimDX occurs.

            mEffect = mKernel.Get<IEffectCompiler>()
                //.Compile("NormalLighting10.fx");
                .Compile("MyTextureShader.fx");

            mRenderer = mKernel.Get<IObjectRenderer>(
                new ConstructorArgument("effect", mEffect),
                new ConstructorArgument("bufferBindings", bufferBindings));

            mWorldViewProjectionParameter = mKernel.Get<SemanticEffectParameter<Matrix>>(
                new ConstructorArgument("semanticName", "WorldViewProjection"));

            var texture = mKernel.Get<ITexture>(
                new ConstructorArgument("filename", "texture.png"));
            texture.Load();

            mTextureParameter = mKernel.Get<SemanticEffectParameter<ITexture>>(
                new ConstructorArgument("semanticName", "Texture"),
                new ConstructorArgument("texture", texture));

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

            keyboard.On(Input.Button.Escape).WasPressed().Do(Application.Exit);
            keyboard.On(Input.Button.P).WasPressed().Do(() => mRenderWindow.TakeScreenshot("screenshot.bmp"));
        }
    }
}