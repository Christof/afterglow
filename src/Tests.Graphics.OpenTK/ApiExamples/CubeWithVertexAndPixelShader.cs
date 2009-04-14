/* Licensed under the MIT/X11 license.
 * Copyright (c) 2006-2008 the OpenTK Team.
 * This notice may not be removed from any source distribution.
 * See license.txt for licensing detailed licensing details.
 */

using System.Drawing;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Math;
using System;
using System.IO;

namespace Afterglow.Graphics.OpenTK.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class CubeWithVertexAndPixelShader
    {
        static float angle = 0.0f, rotation_speed = 3.0f;
        int vertex_shader_object, fragment_shader_object, shader_program;
        int vertex_buffer_object, color_buffer_object, element_buffer_object;

        Shape shape = new Cube();

        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        [Test]
        public void Run()
        {
            // Check for necessary capabilities:
            if (!GL.SupportsExtension("VERSION_2_0"))
            {
                MessageBox.Show("You need at least OpenGL 2.0 to run this example. Aborting.", "GLSL not supported",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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


                CreateVBO();

                using (StreamReader vs = new StreamReader("Data/Shaders/Simple_VS.glsl"))
                using (StreamReader fs = new StreamReader("Data/Shaders/Simple_FS.glsl"))
                    CreateShaders(vs.ReadToEnd(), fs.ReadToEnd(),
                        out vertex_shader_object, out fragment_shader_object,
                        out shader_program);


                Application.Idle +=
                    delegate
                    {
                        mGLControl.MakeCurrent();

                        GL.ClearColor(mGLControl.BackColor);


                        GL.Enable(EnableCap.DepthTest);
                        GL.Clear(ClearBufferMask.ColorBufferBit);

                        double scaleFactor = 1.0f;
                        OnRenderFrame(scaleFactor);

                        mGLControl.SwapBuffers();
                    };

                Application.Run(form);
            }
        }

        
        #region CreateShaders

        void CreateShaders(string vs, string fs,
            out int vertexObject, out int fragmentObject, 
            out int program)
        {
            int status_code;
            string info;

            vertexObject = GL.CreateShader(ShaderType.VertexShader);
            fragmentObject = GL.CreateShader(ShaderType.FragmentShader);

            // Compile vertex shader
            GL.ShaderSource(vertexObject, vs);
            GL.CompileShader(vertexObject);
            GL.GetShaderInfoLog(vertexObject, out info);
            GL.GetShader(vertexObject, ShaderParameter.CompileStatus, out status_code);

            if (status_code != 1)
                throw new Exception(info);

            // Compile vertex shader
            GL.ShaderSource(fragmentObject, fs);
            GL.CompileShader(fragmentObject);
            GL.GetShaderInfoLog(fragmentObject, out info);
            GL.GetShader(fragmentObject, ShaderParameter.CompileStatus, out status_code);
            
            if (status_code != 1)
                throw new Exception(info);

            program = GL.CreateProgram();
            GL.AttachShader(program, fragmentObject);
            GL.AttachShader(program, vertexObject);

            GL.LinkProgram(program);
            GL.UseProgram(program);
        }

        #endregion

        #region private void CreateVBO()

        void CreateVBO()
        {
            int size;

            GL.GenBuffers(1, out vertex_buffer_object);
            GL.GenBuffers(1, out color_buffer_object);
            GL.GenBuffers(1, out element_buffer_object);

            // Upload the vertex buffer.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(shape.Vertices.Length * 3 * sizeof(float)), shape.Vertices,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (size != shape.Vertices.Length * 3 * sizeof(Single))
                throw new ApplicationException(String.Format(
                    "Problem uploading vertex buffer to VBO (vertices). Tried to upload {0} bytes, uploaded {1}.",
                    shape.Vertices.Length * 3 * sizeof(Single), size));

            // Upload the color buffer.
            GL.BindBuffer(BufferTarget.ArrayBuffer, color_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(shape.Colors.Length * sizeof(int)), shape.Colors,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (size != shape.Colors.Length * sizeof(int))
                throw new ApplicationException(String.Format(
                    "Problem uploading vertex buffer to VBO (colors). Tried to upload {0} bytes, uploaded {1}.",
                    shape.Colors.Length * sizeof(int), size));
            
            // Upload the index buffer (elements inside the vertex buffer, not color indices as per the IndexPointer function!)
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, element_buffer_object);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(shape.Indices.Length * sizeof(Int32)), shape.Indices,
                BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (size != shape.Indices.Length * sizeof(int))
                throw new ApplicationException(String.Format(
                    "Problem uploading vertex buffer to VBO (offsets). Tried to upload {0} bytes, uploaded {1}.",
                    shape.Indices.Length * sizeof(int), size));
        }

        #endregion


        /// <summary>
        /// Place your rendering code here.
        /// </summary>
        public void OnRenderFrame(double scaleFactor)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Glu.LookAt(0.0, 5.0, 5.0,
                       0.0, 0.0, 0.0,
                       0.0, 1.0, 0.0);

            angle += rotation_speed * (float)scaleFactor;
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            GL.EnableClientState(EnableCap.VertexArray);
            GL.EnableClientState(EnableCap.ColorArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
            GL.BindBuffer(BufferTarget.ArrayBuffer, color_buffer_object);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, IntPtr.Zero);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, element_buffer_object);

            GL.DrawElements(BeginMode.Triangles, shape.Indices.Length,
                DrawElementsType.UnsignedInt, IntPtr.Zero);

            //GL.DrawArrays(GL.Enums.BeginMode.POINTS, 0, shape.Vertices.Length);

            GL.DisableClientState(EnableCap.VertexArray);
            GL.DisableClientState(EnableCap.ColorArray);


            //int error = GL.GetError();
            //if (error != 0)
            //    Debug.Print(Glu.ErrorString(Glu.Enums.ErrorCode.INVALID_OPERATION));
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
}