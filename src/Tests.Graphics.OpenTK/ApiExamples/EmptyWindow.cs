using System.Drawing;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;

namespace Afterglow.Graphics.OpenTK.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class EmptyWindow
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        [Test]
        public void Run()
        {
            using (var form = CreateForm())
            {
                GLControl mGLControl = new GLControl
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                    BackColor = Color.LightGreen,
                    Location = new Point(1, 0),
                    Name = "GL Control",
                    Size = new Size(WIDTH, HEIGHT),
                    TabIndex = 0,
                    VSync = false
                };
                form.Controls.Add(mGLControl);

                Application.Idle +=
                    delegate
                    {
                        mGLControl.MakeCurrent();

                        GL.ClearColor(mGLControl.BackColor);
                        GL.Clear(ClearBufferMask.ColorBufferBit);

                        mGLControl.SwapBuffers();
                    };

                Application.Run(form);
            }
        }
       
        /// <summary>
        /// Creates a form with a set client size.
        /// </summary>
        /// <returns>The new form.</returns>
        public static Form CreateForm()
        {
            return new Form
            {
                ClientSize = new System.Drawing.Size(WIDTH, HEIGHT)
            };
        }
    }
}