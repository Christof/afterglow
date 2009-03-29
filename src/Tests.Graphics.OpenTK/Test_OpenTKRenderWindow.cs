using System;
using System.Drawing;
using System.Windows.Forms;
using MbUnit.Framework;
using Afterglow.Infrastructure;

namespace Afterglow.Graphics.OpenTK
{
    [Category(Categories.EXAMPLES)]
    public class Test_OpenTKRenderWindow
    {
        [Test]
        public void Run()
        {
            using (var form = new Form())
            {
                form.ClientSize = new Size(800, 600);

                using (var renderWindow = new OpenTKRenderWindow(form))
                {
                    form.Controls[0].KeyPress +=
                        delegate(object sender, KeyPressEventArgs args)
                        {
                            if (args.KeyChar == 'p')
                            {
                                renderWindow.TakeScreenshot("testOpenTK.bmp");
                                Console.WriteLine("Successfully took screenshot!");
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