using System.Windows.Forms;
using MbUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheNewEngine.Graphics.Xna.ApiExamples
{
    [TestFixture]
    public class TriangleWithVertexBuffer
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Vector3 Color;
        }

        [Test]
        [Category(Categories.API_EXAMPLES)]
        public void Run()
        {
            using (var form = EmptyWindow.CreateForm())
            {
                PresentationParameters presentationParameters = EmptyWindow.CreatePresentationParameters();

                var device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware,
                    form.Handle, presentationParameters);

                Vertex[] vertices = CreateVertices();

                var vertexBuffer = new VertexBuffer(device,
                    typeof(Vertex), vertices.Length, BufferUsage.None);

                vertexBuffer.SetData(vertices, 0, vertices.Length);

                VertexDeclaration vertexDeclaration = CreateVertexDeclaration(device);

                var basicEffect = new BasicEffect(device, new EffectPool())
                {
                    LightingEnabled = false,
                    TextureEnabled = false,
                    VertexColorEnabled = true
                };

                Application.Idle +=
                    delegate
                    {
                        device.Clear(Color.Blue);

                        device.VertexDeclaration = vertexDeclaration;
                        device.Vertices[0].SetSource(vertexBuffer, 0, 24);

                        basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
                            (float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                        basicEffect.View = Matrix.CreateLookAt(
                            new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                        basicEffect.World = Matrix.Identity;

                        RenderPrimitive(device, basicEffect);

                        device.Present();

                        Application.DoEvents();
                    };

                Application.Run(form);
            }
        }

        /// <summary>
        /// Renders one primitive with the given effect.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="effect">The effect.</param>
        public static void RenderPrimitive(GraphicsDevice device, Effect effect)
        {
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                pass.End();
            }
            effect.End();
        }

        private static VertexDeclaration CreateVertexDeclaration(GraphicsDevice device)
        {
            return new VertexDeclaration(
                device, new[]
                {
                    new VertexElement(
                        0, 0, VertexElementFormat.Vector3,
                        VertexElementMethod.Default, VertexElementUsage.Position, 0),
                    new VertexElement(
                        0, 12, VertexElementFormat.Vector3,
                        VertexElementMethod.Default, VertexElementUsage.Color, 0)
                });
        }

        private static Vertex[] CreateVertices()
        {
            var top = new Vertex { Position = new Vector3(0f, 1f, 0f), Color = new Vector3(0.5f, 1f, 0f) };
            var left = new Vertex { Position = new Vector3(-1f, -1f, 0f), Color = new Vector3(0f, 0f, 0f) };
            var right = new Vertex { Position = new Vector3(1f, -1f, 0f), Color = new Vector3(1f, 0f, 0f) };

            return new[] { top, left, right };
        }
    }
}