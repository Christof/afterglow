using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Drawing;
using Afterglow.Graphics.Utilities;
using MbUnit.Framework;

namespace Afterglow.Graphics
{
    public abstract class SceneTestBase : IScene
    {
        protected Form Form { get; private set; }

        [SetUp]
        public void Setup()
        {
            Form = new Form
            {
                ClientSize = new Size(800, 600)
            };
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

                        Update(new FrameInfo(frametime, now / 1000.0f));
                        Render();

                        //AssertWithScreenshot();
                    }
                };
        }

        private void AssertWithScreenshot(IScreenshotTaker screenshotTaker)
        {
            var name = GetType().Name;
            var expected = name + "_expected.bmp";
            var actual = name + "_actual.bmp";
            if (File.Exists(expected))
            {
                screenshotTaker.TakeScreenshot(actual);

                Assert.IsTrue(ImageComparer.Compare(expected, actual));
            }
            else
            {
                screenshotTaker.TakeScreenshot(expected);
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
        /// <param name="frameInfo">The frame info.</param>
        public abstract void Update(IFrameInfo frameInfo);

        /// <summary>
        /// Renders the scene.
        /// </summary>
        public abstract void Render();
    }
}