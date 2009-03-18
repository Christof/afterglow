using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Math;

namespace Afterglow.Graphics.OpenTK.ApiExamples
{
    /// <summary>
    /// http://www.opentk.com/doc/chapter/2/opengl/geometry/vbo
    /// </summary>
    [TestFixture]
    public class QuadWithBuffers
    {
        private const int HEIGHT = 600;

        private const int WIDTH = 800;

        private uint[] vertexBufferIds;

        [Test]
        [Category(Categories.API_EXAMPLES)]
        public void Run()
        {
            using (Form form = CreateForm())
            {
                var mGLControl = new GLControl
                {
                    Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right))),
                    BackColor = Color.LightGreen,
                    Location = new Point(1, 0),
                    Name = "GL Control",
                    Size = new Size(WIDTH, HEIGHT),
                    TabIndex = 0,
                    VSync = false
                };
                form.Controls.Add(mGLControl);

                //have to set this to get a -1 to 1 system view
                GL.Viewport(0, 0, WIDTH, HEIGHT);

                //lets create 2 buffers and get their ids
                vertexBufferIds = new uint[2];
                GL.GenBuffers(2, vertexBufferIds);
                
                var mPositions = new[]
                {
                    new Vector3(-1, -1, 0),
                    new Vector3(1, -1, 0),
                    new Vector3(1, 1, 0),
                    new Vector3(-1, 1, 0),
                };
                var mColors = new[]
                {
                    Color.Red, Color.Green,
                    Color.Blue, Color.Yellow
                };

                Application.Idle +=
                    delegate
                    {
                        mGLControl.MakeCurrent();

                        GL.ClearColor(mGLControl.BackColor);
                        GL.Clear(ClearBufferMask.ColorBufferBit);

                        RenderFrame();

                        mGLControl.SwapBuffers();
                    };

                form.BringToFront();
                Application.Run(form);

                //Free the data
                GL.DeleteBuffers(2, vertexBufferIds);
            }
        }

        private void RenderFrame()
        {
            GL.EnableClientState(EnableCap.VertexArray);

            GL.Begin(BeginMode.Quads);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferIds[0]);
   
            GL.End();
        }


        /// <summary>
        /// Creates a form with a set client size.
        /// </summary>
        /// <returns>The new form.</returns>
        public static Form CreateForm()
        {
            return new Form
            {
                ClientSize = new Size(WIDTH, HEIGHT),
                Visible = true
            };
        }
    }
}