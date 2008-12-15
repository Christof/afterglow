using System.Windows.Forms;
using Gallio.Framework;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using TheNewEngine.Graphics.SlimDX.GraphicStreams;
using Device = SlimDX.Direct3D10.Device;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class TriangleWithTexture
    {
        [Test]
        [Category(Categories.EXAMPLES)]
        public void Run()
        {
            using (var form = EmptyWindow.CreateForm())
            {
                Device device;
                SwapChain swapChain;
                RenderTargetView renderTarget;

                EmptyWindow.CreateDeviceSwapChainAndRenderTarget(form, out device, out swapChain, out renderTarget);

                var container = new GraphicStreamContainer();
                container.Create(GraphicStreamUsage.Position, CreatePositions());
                container.Create(GraphicStreamUsage.Color, CreateColors());

                var containerImplementation = new BufferContainer(device);
                container.Load(containerImplementation);

                Effect effect;
                string errors = string.Empty;
                try
                {
                    effect = Effect.FromFile(device, "MyTextureShader10.fx", "fx_4_0",
                        ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
                    System.Console.WriteLine(errors);
                }
                catch (System.Exception)
                {
                    TestLog.Warnings.WriteLine(errors);

                    throw;
                }

                var technique = effect.GetTechniqueByIndex(0);
                var pass = technique.GetPassByIndex(0);

                var inputLayout = new InputLayout(device, containerImplementation.InputElements, pass.Description.Signature);

                Application.Idle +=
                    delegate
                    {
                        device.ClearRenderTargetView(renderTarget, new Color4(0, 0, 0));

                        device.InputAssembler.SetInputLayout(inputLayout);
                        device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                       
                        container.OnFrame();

                        Matrix view = Matrix.LookAtRH(new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                        Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                        Matrix world = Matrix.Identity;
                        Matrix worldViewProjection = world * view * projection;

                        effect.GetVariableBySemantic("WorldViewProjection")
                            .AsMatrix().SetMatrix(worldViewProjection);

                        for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                        {
                            pass.Apply();

                            // TODO
                            device.Draw(3, 0);
                        }

                        swapChain.Present(0, PresentFlags.None);

                        Application.DoEvents();
                    };

                Application.Run(form);
            }
        }

        private static Math.Vector3[] CreatePositions()
        {
            var top = new Math.Vector3(0f, 1f, 0f);
            var left = new Math.Vector3(-1f, -1f, 0f);
            var right = new Math.Vector3(1f, -1f, 0f);

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