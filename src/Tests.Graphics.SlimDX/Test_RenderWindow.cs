using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.SlimDX
{
    public class Test_RenderWindow
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