using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;

namespace Afterglow.Graphics
{
    public class Test_SlimDXRenderWindow
    {
        [Test]
        public void Run()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new SlimDXRenderWindow(form.Handle))
                {
                    form.KeyPress +=
                        delegate(object sender, KeyPressEventArgs args)
                        {
                            if (args.KeyChar == 'p')
                            {
                                renderWindow.TakeScreenshot("testSlimDX.bmp");
                            }
                            else if (args.KeyChar == 'f')
                            {
                                renderWindow.SwitchFullscreen();
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