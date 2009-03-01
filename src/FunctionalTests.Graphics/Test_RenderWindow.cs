using System.Drawing;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using System;

namespace Afterglow.Graphics
{
    [TestFixture]
    public class Test_RenderWindow
    {
        [Test]
        public void CanSwitchBetweenRenderWindows()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                IRenderWindow renderWindow = new XnaRenderWindow(form.Handle);

                form.KeyPress +=
                    delegate(object sender, KeyPressEventArgs args)
                    {
                        if (args.KeyChar == 'p')
                        {
                            renderWindow.TakeScreenshot("test.bmp");
                        }
                        else if (args.KeyChar == 's')
                        {
                            if (renderWindow is XnaRenderWindow)
                            {
                                renderWindow.Dispose();
                                renderWindow = new SlimDXRenderWindow(form.Handle);
                            }
                            else
                            {
                                renderWindow.Dispose();
                                renderWindow = new XnaRenderWindow(form.Handle);
                            }
                        }
                    };

                Application.Idle +=
                    delegate
                    {
                        renderWindow.Render();

                        Application.DoEvents();
                    };

                Application.Run(form);

                if (renderWindow != null)
                {
                    renderWindow.Dispose();
                }
            }
        }

        [Test]
        public void CanSwitchBetweenRenderWindowsWhenRenderingIsDoneInAControl()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(1024, 760);

                var button = new Button { Size = new Size(800, 600), Top = 112, Left = 80, Parent = form };

                IntPtr handle = button.Handle;
                IRenderWindow renderWindow = new XnaRenderWindow(handle);

                button.KeyPress +=
                    delegate(object sender, KeyPressEventArgs args)
                    {
                        if (args.KeyChar == 'p')
                        {
                            renderWindow.TakeScreenshot("test.bmp");
                        }
                        else if (args.KeyChar == 's')
                        {
                            if (renderWindow is XnaRenderWindow)
                            {
                                renderWindow.Dispose();
                                renderWindow = new SlimDXRenderWindow(handle);
                            }
                            else
                            {
                                renderWindow.Dispose();
                                renderWindow = new XnaRenderWindow(handle);
                            }
                        }
                    };

                Application.Idle +=
                    delegate
                    {
                        renderWindow.Render();

                        Application.DoEvents();
                    };
                Application.Run(form);

                if (renderWindow != null)
                {
                    renderWindow.Dispose();
                }
            }
        }

        [Test]
        [Ignore("Implement dependency injection first.")]
        public void WithDependencyInjection()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);
                
                var renderWindow = DependencyResolver.ResolveWith<IRenderWindow>(
                    "control", form); //new Xna.XnaRenderWindow(form.Handle);

                form.KeyPress +=
                    delegate(object sender, KeyPressEventArgs args)
                    {
                        if (args.KeyChar == 'p')
                        {
                            renderWindow.TakeScreenshot("test.bmp");
                        }
                        else if (args.KeyChar == 's')
                        {
                            renderWindow.Dispose();

                            //ObjectFactory.Profile = ObjectFactory.Profile == "Xna" ? "SlimDX" : "Xna";

                            renderWindow = DependencyResolver.ResolveWith<IRenderWindow>(
                                "windowHandle", form.Handle);
//                            if (renderWindow is Xna.XnaRenderWindow)
//                            {
//                                renderWindow.Dispose();
//                                renderWindow = new SlimDX.SlimDXRenderWindow(form.Handle);
//                            }
//                            else
//                            {
//                                renderWindow.Dispose();
//                                renderWindow = new Xna.XnaRenderWindow(form.Handle);
//                            }
                        }
                    };

                Application.Idle +=
                    delegate
                    {
                        renderWindow.Render();

                        Application.DoEvents();
                    };

                Application.Run(form);

                if (renderWindow != null)
                {
                    renderWindow.Dispose();
                }
            }
        }
    }
}