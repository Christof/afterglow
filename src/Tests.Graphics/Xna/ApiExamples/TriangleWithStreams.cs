using System;
using System.Windows.Forms;
using MbUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheNewEngine.Graphics.Xna.ApiExamples
{
    [TestFixture]
    public class TriangleWithStreams
    {
        [Test]
        [Category(Categories.API_EXAMPLES)]
        public void Run()
        {
            using (Form form = EmptyWindow.CreateForm())
            {
                PresentationParameters presentationParameters = EmptyWindow.CreatePresentationParameters();

                var device = new GraphicsDevice(
                    GraphicsAdapter.DefaultAdapter, DeviceType.Hardware,
                    form.Handle, presentationParameters);

                var positions = new[] { new Vector3(0f, 1f, 0f), new Vector3(-1f, -1f, 0f), new Vector3(1f, -1f, 0f) };
                var colors = new[] { new Vector3(0.5f, 1f, 0f), new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f) };

                var positionsBuffer = new VertexBuffer(device,
                    typeof(Vector3), positions.Length, BufferUsage.None);

                var colorsBuffer = new VertexBuffer(device,
                    typeof(Vector3), colors.Length, BufferUsage.None);

                positionsBuffer.SetData(positions, 0, positions.Length);
                colorsBuffer.SetData(colors, 0, colors.Length);

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
                        device.Vertices[0].SetSource(positionsBuffer, 0, 12);
                        device.Vertices[1].SetSource(colorsBuffer, 0, 12);

                        basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
                            (float) (Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                        basicEffect.View = Matrix.CreateLookAt(
                            new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                        basicEffect.World = Matrix.Identity;

                        TriangleWithVertexBuffer.RenderPrimitive(device, basicEffect);

                        device.Present();

                        Application.DoEvents();
                    };

                Application.Run(form);
            }
        }

        /// <summary>
        /// Creates the vertex declaration.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>The vertex declaration.</returns>
        public static VertexDeclaration CreateVertexDeclaration(GraphicsDevice device)
        {
            return new VertexDeclaration(
                device, new[]
                {
                    new VertexElement(
                        0, 0, VertexElementFormat.Vector3,
                        VertexElementMethod.Default, VertexElementUsage.Position, 0),
                    new VertexElement(
                        1, 0, VertexElementFormat.Vector3,
                        VertexElementMethod.Default, VertexElementUsage.Color, 0)
                });
        }
    }
}