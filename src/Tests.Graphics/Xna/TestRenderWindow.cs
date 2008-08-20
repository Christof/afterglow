using System;
using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color=Microsoft.Xna.Framework.Graphics.Color;

namespace TheNewEngine.Graphics.Xna
{
    public class TestRenderWindow
    {
        [Test]
        public void Run()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new RenderWindow(form.Handle))
                {
                    form.KeyPress +=
                        delegate(object sender, KeyPressEventArgs args)
                        {
                            if (args.KeyChar == 'p')
                            {
                                renderWindow.TakeScreenshot("testXna.bmp");
                            }
                        };

                    Application.Idle +=
                        delegate
                        {
                            renderWindow.Render();

                            Application.DoEvents();
                        };

                    Application.Run(form);
                }
            }
        }

        private struct Vertex
        {
            public Vector3 Position;
            public Vector3 Color;
        }

        [Test]
        public void VertexBuffer()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new RenderWindow(form.Handle))
                {
                    renderWindow.Device.RenderState.CullMode = CullMode.CullCounterClockwiseFace;

                    var top = new Vertex {Position = new Vector3(0f, 1f, 0f), Color = new Vector3(0.5f, 1f, 0f)};
                    var left = new Vertex {Position = new Vector3(-1f, -1f, 0f), Color = new Vector3(0f, 0f, 0f)};
                    var right = new Vertex {Position = new Vector3(1f, -1f, 0f), Color = new Vector3(1f, 0f, 0f)};

                    var vertices = new[] {top, left, right};

                    var vertexBuffer = new VertexBuffer(
                        renderWindow.Device,
                        typeof (Vertex), vertices.Length, BufferUsage.None);

                    vertexBuffer.SetData(vertices, 0, vertices.Length);

                    var vertexDeclaration = new VertexDeclaration(
                        renderWindow.Device, new[]
                        {
                            new VertexElement(
                                0, 0, VertexElementFormat.Vector3,
                                VertexElementMethod.Default, VertexElementUsage.Position, 0),
                            new VertexElement(
                                0, 12, VertexElementFormat.Vector3,
                                VertexElementMethod.Default, VertexElementUsage.Color, 0)
                        });

                    var basicEffect = new BasicEffect(renderWindow.Device, new EffectPool());
                    basicEffect.LightingEnabled = false;
                    basicEffect.TextureEnabled = false;
                    basicEffect.VertexColorEnabled = true;

                    renderWindow.RenderAction +=
                        delegate
                        {
                            renderWindow.Device.VertexDeclaration = vertexDeclaration;
                            renderWindow.Device.Vertices[0].SetSource(vertexBuffer, 0, 24);

                            Matrix view = Matrix.CreateLookAt(
                                new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                                (float) (System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                            Matrix world = Matrix.Identity; //Matrix.CreateRotationY(1.2f);

                            basicEffect.Projection = projection;
                            basicEffect.View = view;
                            basicEffect.World = world;

                            basicEffect.Begin();
                            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                            {
                                pass.Begin();

                                renderWindow.Device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                                pass.End();
                            }
                            basicEffect.End();

                        };

                    form.KeyPress +=
                        delegate(object sender, KeyPressEventArgs args)
                        {
                            if (args.KeyChar == 'p')
                            {
                                renderWindow.TakeScreenshot("testXna.bmp");
                            }
                        };

                    Application.Idle +=
                        delegate
                        {
                            renderWindow.Render();

                            Application.DoEvents();
                        };

                    Application.Run(form);
                }
            }
        }

        [Test]
        public void VertexBufferWithStreams()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new RenderWindow(form.Handle))
                {
                    renderWindow.Device.RenderState.CullMode = CullMode.CullCounterClockwiseFace;
                    var positions = new[] { new Vector3(0f, 1f, 0f), new Vector3(-1f, -1f, 0f), new Vector3(1f, -1f, 0f) };
                    var colors = new[] { new Vector3(0.5f, 1f, 0f), new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f) };

                    var positionsBuffer = new VertexBuffer(renderWindow.Device,
                        typeof(Vector3), positions.Length, BufferUsage.None);

                    var colorsBuffer = new VertexBuffer(renderWindow.Device,
                        typeof(Vector3), colors.Length, BufferUsage.None);

                    positionsBuffer.SetData(positions, 0, positions.Length);
                    colorsBuffer.SetData(colors, 0, colors.Length);

                    var vertexDeclaration = new VertexDeclaration(
                        renderWindow.Device, new []
                        {
                            new VertexElement(0, 0, VertexElementFormat.Vector3,
                                VertexElementMethod.Default, VertexElementUsage.Position, 0 ),
                            new VertexElement(1, 0, VertexElementFormat.Vector3,
                                VertexElementMethod.Default, VertexElementUsage.Color, 0 )
                        });

                    var basicEffect = new BasicEffect(renderWindow.Device, new EffectPool());
                    basicEffect.LightingEnabled = false;
                    basicEffect.TextureEnabled = false;
                    basicEffect.VertexColorEnabled = true;

                    renderWindow.RenderAction += 
                        delegate 
                        {
                            renderWindow.Device.VertexDeclaration = vertexDeclaration;
                            renderWindow.Device.Vertices[0].SetSource(positionsBuffer, 0, 12);
                            renderWindow.Device.Vertices[1].SetSource(colorsBuffer, 0, 12);
                            
                            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                            Matrix projection = Matrix.CreatePerspectiveFieldOfView((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                            Matrix world = Matrix.Identity;//Matrix.CreateRotationY(1.2f);

                            basicEffect.Projection = projection;
                            basicEffect.View = view;
                            basicEffect.World = world;

                            basicEffect.Begin();
                            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                            {
                                pass.Begin();

                                renderWindow.Device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                                pass.End();
                            }
                            basicEffect.End();
                            
                        };

                    form.KeyPress +=
                        delegate(object sender, KeyPressEventArgs args)
                        {
                            if (args.KeyChar == 'p')
                            {
                                renderWindow.TakeScreenshot("testXna.bmp");
                            }
                        };

                    Application.Idle +=
                        delegate
                        {
                            renderWindow.Render();

                            Application.DoEvents();
                        };

                    Application.Run(form);
                }
            }
        }

        [Test]
        public void CustomEffect()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new RenderWindow(form.Handle))
                {
                    renderWindow.Device.RenderState.CullMode = CullMode.CullCounterClockwiseFace;
                    var positions = new[] { new Vector3(0f, 1f, 0f), new Vector3(-1f, -1f, 0f), new Vector3(1f, -1f, 0f) };
                    var colors = new[] { new Vector3(0.5f, 1f, 0f), new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f) };

                    var positionsBuffer = new VertexBuffer(renderWindow.Device,
                        typeof(Vector3), positions.Length, BufferUsage.None);
                    
                    var colorsBuffer = new VertexBuffer(renderWindow.Device,
                        typeof(Vector3), colors.Length, BufferUsage.None);

                    positionsBuffer.SetData(positions, 0, positions.Length);
                    colorsBuffer.SetData(colors, 0, colors.Length);

                    var vertexDeclaration = new VertexDeclaration(
                        renderWindow.Device, new[]
                        {
                            new VertexElement(0, 0, VertexElementFormat.Vector3,
                                VertexElementMethod.Default, VertexElementUsage.Position, 0 ),
                            new VertexElement(1, 0, VertexElementFormat.Vector3,
                                VertexElementMethod.Default, VertexElementUsage.Color, 0 )
                        });

                    CompiledEffect compiledEffect = Effect.CompileEffectFromFile(
                        "MyShader.fx", null, null, CompilerOptions.Debug, TargetPlatform.Windows);

                    if (compiledEffect.Success == false)
                    {
                        throw new Exception(compiledEffect.ErrorsAndWarnings);
                    }

                    var effect = new Effect(renderWindow.Device, compiledEffect.GetEffectCode(),
                        CompilerOptions.Debug, new EffectPool());

                    float angle = 0.0f;

                    renderWindow.RenderAction +=
                        delegate
                        {
                            renderWindow.Device.VertexDeclaration = vertexDeclaration;
                            renderWindow.Device.Vertices[0].SetSource(positionsBuffer, 0, 12);
                            renderWindow.Device.Vertices[1].SetSource(colorsBuffer, 0, 12);

                            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, -3), new Vector3(), new Vector3(0, 1, 0));
                            Matrix projection = Matrix.CreatePerspectiveFieldOfView((float)(System.Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                            Matrix world = Matrix.CreateRotationY(angle);
                            angle += 0.1f;

                            effect.Parameters.GetParameterBySemantic("WorldViewProjection").SetValue(
                                world * view * projection);

                            effect.Begin();
                            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                            {
                                pass.Begin();
                                
                                renderWindow.Device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                                pass.End();
                            }
                            effect.End();

                        };

                    form.KeyPress +=
                        delegate(object sender, KeyPressEventArgs args)
                        {
                            if (args.KeyChar == 'p')
                            {
                                renderWindow.TakeScreenshot("testXna.bmp");
                            }
                        };

                    Application.Idle +=
                        delegate
                        {
                            renderWindow.Render();

                            Application.DoEvents();
                        };

                    Application.Run(form);
                }
            }
        }
    }
}