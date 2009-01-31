using System.IO;
using System.Windows.Forms;
using MbUnit.Framework;
using TheNewEngine.Graphics.Utilities;
using System.Diagnostics;

namespace TheNewEngine.Graphics.SlimDX
{
    public abstract class SceneTestBase : IScene
    {
        public Form Form { get; set; }

        public SlimDXRenderWindow RenderWindow { get; set; }

        [SetUp]
        public void Setup()
        {
            Form = new Form
            {
                ClientSize = new System.Drawing.Size(800, 600)
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

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            long last = stopwatch.ElapsedMilliseconds;

            Application.Idle +=
                delegate
                {
                    long now = stopwatch.ElapsedMilliseconds;
                    float frametime = (now - last) / 1000.0f;
                    last = now;

                    Update(frametime);
                    Render();

                    //AssertWithScreenshot();

                    Application.DoEvents();
                };

            Application.Run(Form);
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