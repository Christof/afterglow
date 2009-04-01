#region --- License ---

/* Copyright (c) 2006, 2007 Stefanos Apostolopoulos
 * See license.txt for license info
 */

#endregion

#region --- Using directives ---

using System;
using System.Drawing;
using System.Windows.Forms;
using Afterglow.Graphics;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Math;
using OpenTK.Platform;

#endregion

namespace Examples.Tutorial
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class VertexBuffers
    {
        [Test]
        public void Run()
        {
            using (var example = new T08_VBO())
            {
                example.Run(30.0, 0.0);
            }
        }
    }

    public static class Utilities
    {
        /// <summary>
        /// Converts a System.Drawing.Color to a System.Int32.
        /// </summary>
        /// <param name="c">The System.Drawing.Color to convert.</param>
        /// <returns>A System.Int32 containing the R, G, B, A values of the
        /// given System.Drawing.Color in the Rbga32 format.</returns>
        public static int ColorToRgba32(Color c)
        {
            return ((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
        }
    }

    public abstract class Shape
    {
        public Vector3[] Vertices { get; protected set; }

        public Vector3[] Normals { get; protected set; }

        public Vector2[] Texcoords { get; protected set; }

        public int[] Indices { get; protected set; }

        public int[] Colors { get; protected set; }
    }

    public class Cube : Shape
    {
        public Cube()
        {
            Vertices = new[]
            {
                new Vector3(-1.0f, -1.0f, 1.0f),
                new Vector3(1.0f, -1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(-1.0f, 1.0f, 1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(1.0f, -1.0f, -1.0f),
                new Vector3(1.0f, 1.0f, -1.0f),
                new Vector3(-1.0f, 1.0f, -1.0f)
            };

            Indices = new[]
            {
                // front face
                0, 1, 2, 2, 3, 0,
                // top face
                3, 2, 6, 6, 7, 3,
                // back face
                7, 6, 5, 5, 4, 7,
                // left face
                4, 0, 3, 3, 7, 4,
                // bottom face
                0, 1, 5, 5, 4, 0,
                // right face
                1, 5, 6, 6, 2, 1,
            };

            Normals = new[]
            {
                new Vector3(-1.0f, -1.0f, 1.0f),
                new Vector3(1.0f, -1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(-1.0f, 1.0f, 1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(1.0f, -1.0f, -1.0f),
                new Vector3(1.0f, 1.0f, -1.0f),
                new Vector3(-1.0f, 1.0f, -1.0f),
            };

            Colors = new[]
            {
                Utilities.ColorToRgba32(Color.DarkRed),
                Utilities.ColorToRgba32(Color.DarkRed),
                Utilities.ColorToRgba32(Color.Gold),
                Utilities.ColorToRgba32(Color.Gold),
                Utilities.ColorToRgba32(Color.DarkRed),
                Utilities.ColorToRgba32(Color.DarkRed),
                Utilities.ColorToRgba32(Color.Gold),
                Utilities.ColorToRgba32(Color.Gold),
            };
        }
    }

    public class T08_VBO : GameWindow
    {
        #region --- Private Fields ---

        public static readonly int order = 8;

        private readonly Shape cube = new Cube();

        //Shapes.Shape plane = new Examples.Shapes.Plane(16, 16, 2.0f, 2.0f);

        private readonly Vbo[] vbo = new Vbo[2];

        private struct Vbo
        {
            public int EboID, NumElements;

            public int VboID;
        }

        //float angle;

        #endregion

        #region --- Constructor ---

        public T08_VBO()
            : base(800, 600)
        {
        }

        #endregion

        #region OnLoad override

        public override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!GL.SupportsExtension("VERSION_1_5"))
            {
                MessageBox.Show(
                    "You need at least OpenGL 1.5 to run this example. Aborting.", "VBOs not supported",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Exit();
            }

            GL.ClearColor(0.1f, 0.1f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            // Create the Vertex Buffer Object:
            // 1) Generate the buffer handles.
            // 2) Bind the Vertex Buffer and upload your vertex buffer. Check that the buffer was uploaded correctly.
            // 3) Bind the Index Buffer and upload your index buffer. Check that the buffer was uploaded correctly.

            vbo[0] = LoadVBO(cube.Vertices, cube.Indices);
            vbo[1] = LoadVBO(cube.Vertices, cube.Indices);
        }

        #endregion

        #region OnResize override

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            double ratio = e.Width / (double)e.Height;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Glu.Perspective(45.0, ratio, 1.0, 64.0);
        }

        #endregion

        #region OnUpdateFrame override

        /// <summary>
        /// Prepares the next frame for rendering.
        /// </summary>
        /// <remarks>
        /// Place your control logic here. This is the place to respond to user input,
        /// update object positions etc.
        /// </remarks>
        public override void OnUpdateFrame(UpdateFrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
            {
                Exit();
            }
        }

        #endregion

        #region OnRenderFrame

        public override void OnRenderFrame(RenderFrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Glu.LookAt(
                0.0, 5.0, 5.0,
                0.0, 0.0, 0.0,
                0.0, 1.0, 0.0);

            GL.Color4(Color.Black);
            Draw(vbo[0]);

            SwapBuffers();
        }

        #endregion

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
                BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(int)), indices,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indices.Length * sizeof(int) != size)
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
    }
}