using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;

namespace TheNewEngine.Graphics
{
    [TestFixture]
    public class TestRenderWindow
    {
        [Test]
        public void CanSwitchBetweenRenderWindows()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                RenderWindowBase renderWindow = new Xna.RenderWindow(form.Handle);
                
                form.KeyPress +=
                    delegate(object sender, KeyPressEventArgs args)
                    {
                        if (args.KeyChar == 'p')
                        {
                            renderWindow.TakeScreenshot("test.bmp");
                        }
                        if (args.KeyChar == 's')
                        {
                            if (renderWindow is Xna.RenderWindow)
                            {
                                renderWindow.Dispose();
                                renderWindow = new SlimDX.RenderWindow(form.Handle);
                            }
                            else
                            {
                                renderWindow.Dispose();
                                renderWindow = new Xna.RenderWindow(form.Handle);
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
    }
}