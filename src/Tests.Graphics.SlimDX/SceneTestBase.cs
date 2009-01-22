using System.Windows.Forms;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.SlimDX
{
    public abstract class SceneTestBase : IScene
    {
        protected Form mForm;

        protected SlimDXRenderWindow mRenderWindow;

        [SetUp]
        public void Setup()
        {
            mForm = new Form
            {
                ClientSize = new System.Drawing.Size(800, 600)
            };

            mRenderWindow = new SlimDXRenderWindow(mForm.Handle);
        }

        [TearDown]
        public void TearDown()
        {
            mForm.Dispose();
        }

        [Test]
        [Category(Categories.EXAMPLES)]
        public void Run()
        {
            Load();

            Application.Idle +=
                delegate
                {
                    Update(0.1f);
                    Render();

                    Application.DoEvents();
                };

            Application.Run(mForm);
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