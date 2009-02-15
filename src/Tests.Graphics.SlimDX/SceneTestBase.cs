using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using MbUnit.Framework;
using TheNewEngine.Graphics.Utilities;
using System.Diagnostics;

namespace TheNewEngine.Graphics.SlimDX
{
    public abstract class SceneTestBase : IScene
    {
        protected Form Form { get; private set; }

        protected SlimDXRenderWindow RenderWindow { get; private set; }

        [SetUp]
        public void Setup()
        {
            Form = new Form
            {
                ClientSize = new Size(800, 600)
            };

            RenderWindow = new SlimDXRenderWindow(Form.Handle);
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

            //Form.Load += (s, e) => MainLoop();
            MainLoop();

            Application.Run(Form);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NativeMessage
        {
            public IntPtr hWnd;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
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
                    float frametime = (now - last)/1000.0f;
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
}