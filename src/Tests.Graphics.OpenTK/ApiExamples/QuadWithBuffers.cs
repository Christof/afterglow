using System;
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

                var mPositions = new[] {new Vector3(-1, -1, 0), new Vector3(1, -1, 0), new Vector3(1, 1, 0),};
                var mColors = new[] {new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1),};
                //lets create 2 buffers and get their ids
                vertexBufferIds = new uint[2];
                GL.GenBuffers(2, vertexBufferIds);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, vertexBufferIds[0]);
                GL.BufferData(
                    BufferTarget.ElementArrayBuffer, (IntPtr)(3 * 3 * sizeof (float)), mPositions, BufferUsageHint.StreamDraw);

                //colors
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, vertexBufferIds[1]);
                GL.BufferData(
                    BufferTarget.ElementArrayBuffer, (IntPtr)(3 * 3 * sizeof(float)), mColors, BufferUsageHint.StreamDraw);

                //GL.BufferData(
                //    BufferTarget.ArrayBuffer,
                //    new IntPtr(6 * sizeof (ushort)),
                //    new ushort[]
                //    {0, 1, 3, 0, 3, 2},
                //    BufferUsageHint.StreamDraw
                //    );


                Application.Idle += delegate
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
            GL.EnableClientState(EnableCap.ColorArray);

            GL.Begin(BeginMode.Triangles);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vertexBufferIds[1]);
            GL.ColorPointer(3, ColorPointerType.Float, 0, null);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vertexBufferIds[0]);
            GL.VertexPointer(3, VertexPointerType.Float, 0, null);


            GL.DrawArrays(BeginMode.Triangles, 0, 3);

            //GL.BindBuffer
            //    (
            //    BufferTarget.ArrayBuffer
            //    ,
            //    vertexBufferIds[
            //        1]);

            //disabling these buffers...
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //render other things here...

            GL.DisableClientState(EnableCap.ColorArray);
            GL.DisableClientState(EnableCap.VertexArray);

            GL.End();
        }

        /// <summary>
        /// Creates a form with a set client size.
        /// </summary>
        /// <returns>The new form.</returns>
        public static Form CreateForm()
        {
            return new Form {ClientSize = new Size(WIDTH, HEIGHT), Visible = true};
        }
    }
}