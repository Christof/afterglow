using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using Gallio.Framework;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Buffer = SlimDX.Direct3D10.Buffer;
using Device = SlimDX.Direct3D10.Device;

namespace Afterglow.Graphics.SlimDX.ApiExamples
{
    [TestFixture]
    [Category(Categories.API_EXAMPLES)]
    public class QuadWithVertexAndIndexBuffer
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Color4 Color;
        }

        [Test]
        public void Run()
        {
            using (var form = EmptyWindow.CreateForm())
            {
                Device device;
                SwapChain swapChain;
                RenderTargetView renderTarget;

                EmptyWindow.CreateDeviceSwapChainAndRenderTarget(form, out device, out swapChain, out renderTarget);

                Vertex[] vertices = CreateVertices();

                var stream = new DataStream(vertices.Length * Marshal.SizeOf(typeof(Vertex)), true, true);

                foreach (var vertex in vertices)
                {
                    stream.Write(vertex.Position);
                    stream.Write(vertex.Color);
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
                    SizeInBytes = (vertices.Length * Marshal.SizeOf(typeof (Vertex))),
                    Usage = ResourceUsage.Default
                };

                var buffer = new Buffer(device, stream, bufferDescription);
                stream.Close();

                var indices = CreateIndices();
                var sizeInBytes = indices.Length * Marshal.SizeOf(typeof(uint));
                var indexStream = new DataStream(sizeInBytes, true, true);
                indexStream.WriteRange(indices);
                indexStream.Position = 0;
                var indexBufferDescription = new BufferDescription
                {
                    BindFlags = BindFlags.IndexBuffer,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None,
                    SizeInBytes = sizeInBytes,
                    Usage = ResourceUsage.Default
                };

                var indexBuffer = new Buffer(device, indexStream, indexBufferDescription);
                indexStream.Close();

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
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
                };

                var inputLayout = new InputLayout(device, inputElements, pass.Description.Signature);

                Application.Idle +=
                   delegate
                   {
                       device.ClearRenderTargetView(renderTarget, new Color4(0, 0, 0));

                       device.InputAssembler.SetInputLayout(inputLayout);
                       device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                       device.InputAssembler.SetVertexBuffers(0,
                           new VertexBufferBinding(buffer, Marshal.SizeOf(typeof(Vertex)), 0));
                       device.InputAssembler.SetIndexBuffer(indexBuffer, Format.R32_UInt, 0);

                       Matrix view = Matrix.LookAtRH(new Vector3(0, 0, 3), new Vector3(), new Vector3(0, 1, 0));
                       Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                       Matrix world = Matrix.Identity;
                       Matrix worldViewProjection = world * view * projection;

                       effect.GetVariableBySemantic("WorldViewProjection")
                           .AsMatrix().SetMatrix(worldViewProjection);

                       for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                       {
                           pass.Apply();
                           device.DrawIndexed(indices.Length, 0, 0);
                       }

                       swapChain.Present(0, PresentFlags.None);

                       Application.DoEvents();
                   };

                Application.Run(form);
            }
        }

        private static Vertex[] CreateVertices()
        {
            var topLeft = new Vertex { Position = new Vector3(-1f, 1f, 0f), Color = new Color4(1f, 0f, 0f) };
            var topRight = new Vertex { Position = new Vector3(1f, 1f, 0f), Color = new Color4(0f, 0f, 1f) };
            var bottomLeft = new Vertex { Position = new Vector3(-1f, -1f, 0f), Color = new Color4(0f, 1f, 0f) };
            var bottomRight = new Vertex { Position = new Vector3(1f, -1f, 0f), Color = new Color4(1f, 1f, 0f) };

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2 };
        }
    }
}