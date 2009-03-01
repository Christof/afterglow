using System.Windows.Forms;
using MbUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.Xna.ApiExamples
{
    [TestFixture]
    public class QuadWithVertexAndIndexBuffer
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

                var indices = CreateIndices();
                var indexBuffer = new IndexBuffer(device,
                    sizeof (int) * indices.Length, BufferUsage.None, 
                    IndexElementSize.ThirtyTwoBits);
                indexBuffer.SetData(indices);

                VertexDeclaration vertexDeclaration = TriangleWithVertexBuffer.CreateVertexDeclaration(device);

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
                        device.Indices = indexBuffer;
                        device.RenderState.CullMode = CullMode.None;

                        basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
                            (float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                        basicEffect.View = Matrix.CreateLookAt(
                            new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                        basicEffect.World = Matrix.Identity;

                        basicEffect.Begin();
                        foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                        {
                            pass.Begin();

                            device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                                0, 0, vertices.Length, 0, 2);

                            pass.End();
                        }

                        basicEffect.End();

                        device.Present();

                        Application.DoEvents();
                    };

                Application.Run(form);
            }
        }

        private static Vertex[] CreateVertices()
        {
            var topLeft = new Vertex { Position = new Vector3(-1f, 1f, 0f), Color = new Vector3(1f, 0f, 0f) };
            var topRight = new Vertex { Position = new Vector3(1f, 1f, 0f), Color = new Vector3(0f, 0f, 1f) };
            var bottomLeft = new Vertex { Position = new Vector3(-1f, -1f, 0f), Color = new Vector3(0f, 1f, 0f) };
            var bottomRight = new Vertex { Position = new Vector3(1f, -1f, 0f), Color = new Vector3(1f, 1f, 0f) };

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2 };
        }
    }
}