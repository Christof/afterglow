#region --- License ---

/* Copyright (c) 2006, 2007 Stefanos Apostolopoulos
 * See license.txt for license info
 */

#endregion

#region --- Using directives ---

using System;
using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Math;

#endregion

namespace Afterglow.Graphics.OpenTK.ApiExamples
{
    [TestFixture]
    public class QuadWithBuffers
    {
        private const int HEIGHT = 600;

        private const int WIDTH = 800;

        private Vbo mVbo;

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


                if (!GL.SupportsExtension("VERSION_1_5"))
                {
                    Assert.Fail("You need at least OpenGL 1.5 to run this example. Aborting.", "VBOs not supported");
                }


                var positions = new[]
                {
                    new Vector3(-1, -1, 0),
                    new Vector3(1, -1, 0),
                    new Vector3(1, 1, 0),
                    new Vector3(-1, 1, 0),
                };
                var colors = new[]
                {
                    Color.Red, Color.Green,
                    Color.Blue, Color.Yellow
                };

                var indices = new int[] { 0, 1, 3, 0, 3, 2 };

                // Create the Vertex Buffer Object:
                // 1) Generate the buffer handles.
                // 2) Bind the Vertex Buffer and upload your vertex buffer. Check that the buffer was uploaded correctly.
                // 3) Bind the Index Buffer and upload your index buffer. Check that the buffer was uploaded correctly.

                mVbo = LoadVBO(positions, indices);


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
            GL.Begin(BeginMode.Triangles);

            Draw(this.mVbo);

            GL.End();
        }

        private Vbo LoadVBO(Vector3[] vertices, int[] indices)
        {
            var handle = new Vbo();
            int size;

            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(
                BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Vector3.SizeInBytes), vertices,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (vertices.Length * Vector3.SizeInBytes != size)
            {
                throw new ApplicationException("Vertex array not uploaded correctly");
            }
            //GL.BindBuffer(Version15.ArrayBuffer, 0);

            GL.GenBuffers(1, out handle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof (int)), indices,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indices.Length * sizeof (int) != size)
            {
                throw new ApplicationException("Element array not uploaded correctly");
            }
            //GL.BindBuffer(Version15.ElementArrayBuffer, 0);

            handle.NumElements = indices.Length;
            return handle;
        }

        private void Draw(Vbo handle)
        {
            //GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);

            //GL.EnableClientState(EnableCap.TextureCoordArray);
            GL.EnableClientState(EnableCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);

            //GL.TexCoordPointer(2, TexCoordPointerType.Float, vector2_size, (IntPtr)vector2_size);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);

            GL.DrawElements(BeginMode.Triangles, handle.NumElements, DrawElementsType.UnsignedInt, IntPtr.Zero);
            //GL.DrawArrays(BeginMode.LineLoop, 0, vbo.element_count);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.DisableClientState(EnableCap.VertexArray);
            //GL.DisableClientState(EnableCap.TextureCoordArray);

            //GL.PopClientAttrib();
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

        #region Nested type: Vbo

        private struct Vbo
        {
            public int EboID, NumElements;

            public int VboID;
        }

        #endregion
    }
}