using System.Windows.Forms;
using Gallio.Framework;
using MbUnit.Framework;
using SlimDX;
using SlimDX.Direct3D10;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.SlimDX.ApiExamples;
using TheNewEngine.Graphics.SlimDX.GraphicStreams;
using System.IO;
using TheNewEngine.Graphics.Utilities;

namespace TheNewEngine.Graphics.SlimDX.Examples
{
    [TestFixture]
    public class TriangleWithVertexBuffer
    {
        private Form mForm;

        private RenderWindow mRenderWindow;

        [SetUp]
        public void Setup()
        {
            mForm = EmptyWindow.CreateForm();
            mRenderWindow = new RenderWindow(mForm.Handle);
        }

        [TearDown]
        public void TearDown()
        {
            mForm.Dispose();
        }

        [Test]
        [Category(Categories.EXAMPLES)]
        public void Run()
        {
            var container = new GraphicStreamContainer();
            container.Create(GraphicStreamUsage.Position, CreatePositions());
            container.Create(GraphicStreamUsage.Color, CreateColors());

            var containerImplementation = new BufferContainer(mRenderWindow.Device);
            container.Load(containerImplementation);

            Effect effect;
            string errors = string.Empty;
            try
            {
                effect = Effect.FromFile(mRenderWindow.Device, "MyShader10.fx", "fx_4_0",
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

            var inputLayout = new InputLayout(mRenderWindow.Device, 
                containerImplementation.InputElements, pass.Description.Signature);

            Application.Idle +=
                delegate
                {
                    mRenderWindow.StartRendering();

                    mRenderWindow.Device.InputAssembler.SetInputLayout(inputLayout);
                   
                    container.OnFrame();

                    Matrix view = Matrix.LookAtRH(new Vector3(0, 0, 3), new Vector3(), new Vector3(0, 1, 0));
                    Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                    Matrix world = Matrix.Identity;
                    Matrix worldViewProjection = world * view * projection;

                    effect.GetVariableBySemantic("WorldViewProjection")
                        .AsMatrix().SetMatrix(worldViewProjection);

                    for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                    {
                        pass.Apply();

                        if (container.IndexCount != 0)
                        {
                            mRenderWindow.Device.DrawIndexed(container.IndexCount, 0, 0);
                        }
                        else
                        {
                            mRenderWindow.Device.Draw(container.VertexCount, 0);
                        }
                    }

                    mRenderWindow.Render();

                    //AssertWithScreenshot();

                    Application.DoEvents();
                };

            Application.Run(mForm);
        }

        private void AssertWithScreenshot()
        {
            var name = "TheNewEngine.Graphics.SlimDX.Examples.TriangleWithVertexBuffer.Run";
            var expected = name + "_expected.bmp";
            var actual = name + "_actual.bmp";
            if (File.Exists(expected))
            {
                mRenderWindow.TakeScreenshot(actual);

                Assert.IsTrue(ImageComparer.Compare(expected, actual));
            }
            else
            {
                mRenderWindow.TakeScreenshot(expected);
                Assert.Fail("First run. Excpected image was taken and must be verified");
            }
            Application.Exit();
        }

        private static Math.Vector3[] CreatePositions()
        {
            var top = new Math.Vector3(0f, 1f, 0f);
            var left = new Math.Vector3(-1f, -1f, 0f);
            var right = new Math.Vector3(1f, -1f, 0f);

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