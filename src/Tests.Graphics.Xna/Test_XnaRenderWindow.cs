using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.Xna
{
    public class Test_XnaRenderWindow
    {
        [Test]
        public void Run()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new XnaRenderWindow(form.Handle))
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
                            renderWindow.StartRendering();

                            renderWindow.Render();

                            Application.DoEvents();
                        };

                    Application.Run(form);
                }
            }
        }
    }
}