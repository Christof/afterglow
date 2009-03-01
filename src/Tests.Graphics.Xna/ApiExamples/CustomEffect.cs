using System;
using System.Windows.Forms;
using MbUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.Xna.ApiExamples
{
    [TestFixture]
    public class CustomEffect
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

                VertexDeclaration vertexDeclaration = TriangleWithStreams.CreateVertexDeclaration(device);

                CompiledEffect compiledEffect = Effect.CompileEffectFromFile(
                        "MyShader.fx", null, null, CompilerOptions.Debug, TargetPlatform.Windows);

                if (compiledEffect.Success == false)
                {
                    throw new Exception(compiledEffect.ErrorsAndWarnings);
                }

                var effect = new Effect(device, compiledEffect.GetEffectCode(),
                    CompilerOptions.Debug, new EffectPool());

                Application.Idle +=
                    delegate
                    {
                        device.Clear(Color.Blue);

                        device.VertexDeclaration = vertexDeclaration;
                        device.Vertices[0].SetSource(positionsBuffer, 0, 12);
                        device.Vertices[1].SetSource(colorsBuffer, 0, 12);

                        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                        Matrix projection = Matrix.CreatePerspectiveFieldOfView((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                        Matrix world = Matrix.Identity;

                        effect.Parameters.GetParameterBySemantic("WorldViewProjection").SetValue(
                            world * view * projection);

                        TriangleWithVertexBuffer.RenderPrimitive(device, effect);

                        device.Present();

                        Application.DoEvents();
                    };

                Application.Run(form);
            }
        }
    }
}