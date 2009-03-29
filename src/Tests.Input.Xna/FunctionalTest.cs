using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;

namespace Afterglow.Input.Xna
{
    [Category(Categories.MANUAL)]
    public class FunctionalTest
    {
        [Test]
        public void Test()
        {
            var close = false;
            var form = new Form();
            form.Closing += (s, e) => close = true;
            var xLabel = new Label { Parent = form };
            var yLabel = new Label { Parent = form, Top = 40 };
            var zLabel = new Label { Parent = form, Top = 80 };
            
            var inputDevices = new XnaInputDevices();
            var keyboard = inputDevices.Keyboard;
            keyboard.On(Button.X).IsDown().Do(() => close = true);
            keyboard.On(Button.Escape).WasReleased().Do(() => close = true);
            keyboard.On(Button.W).WasReleased().Do(() => form.Text = "Pressed");
            keyboard.On(Button.S).IsDown().Do(() => form.Text = "Down");
            
            var mouse = inputDevices.Mouse;
            mouse.On(Button.LeftMouse).IsDown().Do(() => form.Text = "Left Mouse down");
            mouse.On(Button.RightMouse).WasReleased().Do(() => form.Text = "Right Mouse was pressed");

            mouse.On(Axis.X).Do(delta => xLabel.Text = "X: " + delta);
            mouse.On(Axis.Y).Do(delta => yLabel.Text = "Y: " + delta);
            mouse.On(Axis.Z).Do(delta => zLabel.Text = "Z: " + delta);

            form.Show();
            
            var timer = new Timer { Interval = 10 };
            timer.Tick += (s, e) => inputDevices.Update();

            timer.Start();

            while (!close)
            {
                Application.DoEvents();
            }
            timer.Stop();
            timer.Dispose();
        }
    }
}