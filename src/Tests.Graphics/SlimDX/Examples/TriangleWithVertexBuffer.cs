using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using Device=SlimDX.Direct3D10.Device;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class TriangleWithVertexBuffer
    {
        [Test]
        [Category(Categories.API_EXAMPLES)]
        public void Run()
        {
            using (var form = EmptyWindow.CreateForm())
            {
                Device device;
                SwapChain swapChain;
                RenderTargetView renderTarget;

                EmptyWindow.CreateDeviceSwapChainAndRenderTarget(form, out device, out swapChain, out renderTarget);

                var positionsBuffer = new Buffer<Vector3>(device, 0);
                var positions = new GraphicStream<Vector3>(GraphicStreamUsage.Position, positionsBuffer);
                positions.SetData(CreatePositions());
                positionsBuffer.SetGraphicStream(positions);
                positions.Load();

                var colorsBuffer = new Buffer<Color4>(device, 1);
                var colors = new GraphicStream<Color4>(GraphicStreamUsage.Color, colorsBuffer);
                colors.SetData(CreateColors());
                colorsBuffer.SetGraphicStream(colors);
                colors.Load();

                Effect effect;
                string errors = string.Empty;
                try
                {
                    effect = Effect.FromFile(device, "MyShader10.fx", "fx_4_0",
                        ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
                    Console.WriteLine(errors);
                }
                catch (Exception)
                {
                    Assert.Warning(errors);

                    throw;
                }

                var technique = effect.GetTechniqueByIndex(0);
                var pass = technique.GetPassByIndex(0);

                var inputElements = new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 0, 1)
                };

                var inputLayout = new InputLayout(device, inputElements, pass.Description.Signature);

                Application.Idle +=
                   delegate
                   {
                       device.ClearRenderTargetView(renderTarget, new Color4(0, 0, 0));

                       device.InputAssembler.SetInputLayout(inputLayout);
                       device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                       
                       positions.OnFrame();
                       colors.OnFrame();

                       Matrix view = Matrix.LookAtRH(new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                       Matrix projection = Matrix.PerspectiveFovRH((float) (Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                       Matrix world = Matrix.Identity;
                       Matrix worldViewProjection = world * view * projection;

                       effect.GetVariableBySemantic("WorldViewProjection")
                           .AsMatrix().SetMatrix(worldViewProjection);

                       for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                       {
                           pass.Apply();
                           device.Draw(positions.Data.Length, 0);
                       }


                       swapChain.Present(0, PresentFlags.None);

                       Application.DoEvents();
                   };

                Application.Run(form);
            }
        }

        private static Vector3[] CreatePositions()
        {
            var top =  new Vector3(0f, 1f, 0f);
            var left = new Vector3(-1f, -1f, 0f);
            var right = new Vector3(1f, -1f, 0f);

            return new[] { top, left, right };
        }

        private static Color4[] CreateColors()
        {
            var top = new Color4(1f, 0f, 0f);
            var left = new Color4(0f, 1f, 0f);
            var right = new Color4(0f, 0f, 1f);

            return new[] { top, left, right };
        }
    }
}