using System;
using System.Drawing;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Math;

namespace Afterglow.Graphics.OpenTK.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class SimpleQuad
    {
        private const int HEIGHT = 600;

        private const int WIDTH = 800;

        private Color[] mColors;

        private Vector3[] mPositions;

        [Test]
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

                mPositions = new[]
                {
                    new Vector3(-1, -1, 0),
                    new Vector3(1, -1, 0),
                    new Vector3(1, 1, 0),
                    new Vector3(-1, 1, 0), 
                };
                mColors = new[]
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
            }
        }

        private void RenderFrame()
        {
            GL.Begin(BeginMode.Quads);

            RenderVertex(0);
            RenderVertex(1);
            RenderVertex(2);
            RenderVertex(3);

            GL.End();
        }

        private void RenderVertex(int index)
        {
            RenderVertex(mPositions[index], mColors[index]);
        }

        private void RenderVertex(Vector3 position, Color color)
        {
            GL.Color4(color);
            GL.Vertex3(position);
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