using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;

namespace Afterglow.Graphics.OpenGL
{
    public class Test_RenderWindow
    {
        [Test]
        [Ignore("Ignore until an OpenGL implementatio exists.")]
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
                                renderWindow.TakeScreenshot("testOpenGL.bmp");
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