using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using Gallio.Framework;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Buffer=SlimDX.Direct3D10.Buffer;
using Device=SlimDX.Direct3D10.Device;

namespace Afterglow.Graphics.SlimDX.ApiExamples
{
    [Category(Categories.API_EXAMPLES)]
    public class DefferedShading
    {
        private Form mForm;

        private struct Vertex
        {
            public Vector3 Position;

            public Color4 Color;

            public Vector3 Normal;
        }

        [SetUp]
        public void Setup()
        {
            mForm = EmptyWindow.CreateForm();
        }

        [TearDown]
        public void TearDown()
        {
            mForm.Dispose();
        }

        [Test]
        public void Run()
        {
            Device device;
            SwapChain swapChain;
            RenderTargetView renderTarget;

            EmptyWindow.CreateDeviceSwapChainAndRenderTarget(mForm, out device, out swapChain, out renderTarget);

            Buffer buffer = CreateBuffer(device);

            var description = new Texture2DDescription
            {
                Format = Format.R8G8B8A8_UNorm, 
                Height = EmptyWindow.HEIGHT, 
                Width = EmptyWindow.WIDTH,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.None,
                BindFlags = BindFlags.RenderTarget,
                ArraySize = 1,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0)
            };

            var color = new Texture2D(device, description);
            var colorView = new RenderTargetView(device, color);

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
                TestLog.Warnings.WriteLine(errors);

                throw;
            }

            var technique = effect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);

            var inputElements = new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 12, 0),
                    new InputElement("NORMAL", 0, Format.R32G32B32_Float, 28, 0),  
                };

            var inputLayout = new InputLayout(device, inputElements, pass.Description.Signature);


            Application.Idle +=
            delegate
            {
                //device.ClearRenderTargetView(renderTarget, new Color4(1, 0, 0));
                device.ClearRenderTargetView(colorView, new Color4(1, 0, 0));
                device.OutputMerger.SetTargets(colorView);

                device.InputAssembler.SetInputLayout(inputLayout);
                device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                device.InputAssembler.SetVertexBuffers(0,
                    new VertexBufferBinding(buffer, Marshal.SizeOf(typeof(Vertex)), 0));

                Matrix view = Matrix.LookAtRH(new Vector3(0, 0, 3), new Vector3(), new Vector3(0, 1, 0));
                Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                Matrix world = Matrix.Identity;
                Matrix worldViewProjection = world * view * projection;

                effect.GetVariableBySemantic("WorldViewProjection")
                    .AsMatrix().SetMatrix(worldViewProjection);

                for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                {
                    pass.Apply();
                    device.Draw(3, 0);
                }

                device.OutputMerger.SetTargets(renderTarget);

                swapChain.Present(0, PresentFlags.None);

                Application.DoEvents();
            };

            Application.Run(mForm);
        }

        private Buffer CreateBuffer(Device device)
        {
            Vertex[] vertices = CreateVertices();

            var stream = new DataStream(vertices.Length * Marshal.SizeOf(typeof(Vertex)), true, true);

            foreach (var vertex in vertices)
            {
                stream.Write(vertex.Position);
                stream.Write(vertex.Color);
                stream.Write(vertex.Normal);
            }

            // Important: when specifying initial buffer data like this, the buffer will
            // read from the current DataStream position; we must rewind the stream to 
            // the start of the data we just wrote.
            stream.Position = 0;

            var bufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = (3 * Marshal.SizeOf(typeof(Vertex))),
                Usage = ResourceUsage.Default
            };

            var buffer = new Buffer(device, stream, bufferDescription);
            stream.Close();
            return buffer;
        }

        private static Vertex[] CreateVertices()
        {
            var top = new Vertex
            {
                Position = new Vector3(0f, 1f, 0f),
                Color = new Color4(1f, 0f, 0f),
                Normal = new Vector3(0, 1, 0)
            };

            var left = new Vertex
            {
                Position = new Vector3(-1f, -1f, 0f),
                Color = new Color4(0f, 1f, 0f),
                Normal = new Vector3(0, 1, 0)
            };

            var right = new Vertex
            {
                Position = new Vector3(1f, -1f, 0f),
                Color = new Color4(0f, 0f, 1f),
                Normal = new Vector3(0, 1, 0)
            };

            return new[] { top, right, left };
        }
    }
}