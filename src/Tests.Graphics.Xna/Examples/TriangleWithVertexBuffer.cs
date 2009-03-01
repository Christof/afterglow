using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Rendering;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.Utilities;
using TheNewEngine.Math;
using System.Windows.Forms;
using System.Drawing;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.Xna.Examples
{
    public abstract class SceneTestBase : IScene
    {
        protected Form Form { get; private set; }

        protected XnaRenderWindow RenderWindow { get; private set; }

        [SetUp]
        public void Setup()
        {
            Form = new Form
            {
                ClientSize = new Size(800, 600)
            };

            RenderWindow = new XnaRenderWindow(Form.Handle);
        }

        [TearDown]
        public void TearDown()
        {
            Form.Dispose();
        }

        [Test]
        [Category(Categories.EXAMPLES)]
        public void Run()
        {
            Load();

            MainLoop();

            Application.Run(Form);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NativeMessage
        {
            public IntPtr HWnd;
            public uint Message;
            public IntPtr WParam;
            public IntPtr LParam;
            public uint Time;
            public Point Point;
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PeekMessage(out NativeMessage message, IntPtr hwnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        private void MainLoop()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            long last = stopwatch.ElapsedMilliseconds;

            Application.Idle +=
                delegate
                {
                    NativeMessage message;
                    while (!PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
                    {
                        long now = stopwatch.ElapsedMilliseconds;
                        float frametime = (now - last) / 1000.0f;
                        last = now;

                        Update(frametime);
                        Render();

                        //AssertWithScreenshot();
                    }
                };
        }

        private void AssertWithScreenshot()
        {
            var name = GetType().Name;
            var expected = name + "_expected.bmp";
            var actual = name + "_actual.bmp";
            if (File.Exists(expected))
            {
                RenderWindow.TakeScreenshot(actual);

                Assert.IsTrue(ImageComparer.Compare(expected, actual));
            }
            else
            {
                RenderWindow.TakeScreenshot(expected);
            }

            Application.Exit();
        }

        /// <summary>
        /// Loads the resources for this scene.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Updates the scene every frame.
        /// </summary>
        /// <param name="timeSinceLastCall">The time since the last call.</param>
        public abstract void Update(float timeSinceLastCall);

        /// <summary>
        /// Renders the scene.
        /// </summary>
        public abstract void Render();
    }

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

            IBufferService bufferService = new XnaBufferService(RenderWindow.Device);

            var bufferBindings = new[]
            {
                bufferService.CreateFor(positions),
                bufferService.CreateFor(colors)
            };

            mEffect = new XnaEffectCompiler(RenderWindow.Device).Compile("MyShader.fx");

            mRenderer = new XnaObjectRenderer(RenderWindow,
                mEffect, bufferBindings);

            mWorldViewProjectionParameter = new XnaMatrixEffectParameter("WorldViewProjection");
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