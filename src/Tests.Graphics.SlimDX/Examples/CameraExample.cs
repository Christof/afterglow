using System.Windows.Forms;
using Gallio.Framework;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using TheNewEngine.Graphics.Cameras;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using TheNewEngine.Graphics.SlimDX.GraphicStreams;
using Device = SlimDX.Direct3D10.Device;
using Vector3 = TheNewEngine.Math.Vector3;
using SlimDXVector = SlimDX.Vector3;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class CameraExample
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
                    effect = Effect.FromFile(device, "MyShader10.fx", "fx_4_0",
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

                var cam = new Camera(new Stand(), new PerspectiveProjectionLense());
                cam.Stand.Position = new Vector3(0, 0, 3);

                form.KeyDown +=
                    delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.W)
                        {
                            cam.Stand.Position += cam.Stand.Direction * 0.1f;
                        }

                        if (e.KeyCode == Keys.S)
                        {
                            cam.Stand.Position -= cam.Stand.Direction * 0.1f;
                        }
                    };

                Application.Idle +=
                    delegate
                    {
                        device.ClearRenderTargetView(renderTarget, new Color4(0, 0, 0));

                        device.InputAssembler.SetInputLayout(inputLayout);
                        device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                       
                        container.OnFrame();

                        effect.GetVariableBySemantic("WorldViewProjection")
                            .AsMatrix().SetMatrix(cam.ViewProjectionMatrix.ToSlimDX());

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

        private static Vector3[] CreatePositions()
        {
            var top = new Vector3(0f, 1f, 0f);
            var left = new Vector3(-1f, -1f, 0f);
            var right = new Vector3(1f, -1f, 0f);

            return new[] { top, right, left };
        }

        private static Color4[] CreateColors()
        {
            var top = new Color4(1f, 0f, 0f);
            var left = new Color4(0f, 1f, 0f);
            var right = new Color4(0f, 0f, 1f);

            return new[] { top, right, left };
        }
    }
}