﻿#region --- License ---

/* Copyright (c) 2006, 2007 Stefanos Apostolopoulos
 * See license.txt for license info
 */

#endregion

#region --- Using directives ---

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Math;

#endregion

namespace Afterglow.Graphics.OpenTK.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class QuadWithBuffers
    {
        private const int HEIGHT = 600;

        private const int WIDTH = 800;

        private int mVertexBufferId;

        private int mIndexBufferId;

        private int mColorBufferId;

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

                var indices = new uint[] { 0, 1, 2, 0, 2, 3 };

                // Create the Vertex Buffer Object:
                // 1) Generate the buffer handles.
                // 2) Bind the Vertex Buffer and upload your vertex buffer. Check that the buffer was uploaded correctly.
                // 3) Bind the Index Buffer and upload your index buffer. Check that the buffer was uploaded correctly.

                LoadVBO(positions, indices, colors);
                
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
            Draw();
        }

        private void LoadVBO(Vector3[] vertices, uint[] indices, Color[] colors)
        {
            int size;

            GL.GenBuffers(1, out mVertexBufferId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, 
                (IntPtr)(vertices.Length * Vector3.SizeInBytes), vertices,
                BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize,
                out size);
            if (vertices.Length * Vector3.SizeInBytes != size)
            {
                throw new ApplicationException("Vertex array not uploaded correctly");
            }

            GL.GenBuffers(1, out mIndexBufferId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mIndexBufferId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, 
                (IntPtr)(indices.Length * sizeof (uint)), indices,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indices.Length * sizeof (int) != size)
            {
                throw new ApplicationException("Element array not uploaded correctly");
            }

            //GL.GenBuffers(1, out mColorBufferId);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, mColorBufferId);
            //GL.BufferData(BufferTarget.ArrayBuffer,
            //    (IntPtr)(colors.Length * Marshal.SizeOf(typeof(Color))), colors,
            //    BufferUsageHint.StaticDraw);

            //GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize,
            //    out size);
            //if (colors.Length * Marshal.SizeOf(typeof(Color)) != size)
            //{
            //    throw new ApplicationException("Vertex array not uploaded correctly");
            //}
        }

        private void Draw()
        {
            GL.Color4(Color.Red);
            //GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);

            //GL.EnableClientState(EnableCap.TextureCoordArray);
            GL.EnableClientState(EnableCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mIndexBufferId);

            //GL.TexCoordPointer(2, TexCoordPointerType.Float, vector2_size, (IntPtr)vector2_size);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);

            var indexCount = 6;
            GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
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
    }
}