using System.Windows.Forms;
using MbUnit.Framework;

namespace Afterglow.Input.SlimDX
{
    public class FunctionalTest
    {
        [Test]
        public void Test()
        {
            var form = new Form();
            var xLabel = new Label { Parent = form };
            var yLabel = new Label { Parent = form, Top = 40 };
            var zLabel = new Label { Parent = form, Top = 80 };

            var close = false;
            var inputDevices = new SlimDXInputDevices(form);
            var keyboard = inputDevices.Keyboard;
            keyboard.On(Button.X).IsDown().Do(() => close = true);
            keyboard.On(Button.Escape).WasPressed().Do(() => close = true);
            keyboard.On(Button.W).WasPressed().Do(() => form.Text = "Pressed");
            keyboard.On(Button.S).IsDown().Do(() => form.Text = "Down");

            var mouse = inputDevices.Mouse;
            mouse.On(Button.LeftMouse).IsDown().Do(() => form.Text = "Left Mouse down");
            mouse.On(Button.RightMouse).WasPressed().Do(() => form.Text = "Right Mouse was pressed");

            mouse.On(Axis.X).Do(delta => xLabel.Text = "X: " + delta);
            mouse.On(Axis.Y).Do(delta => yLabel.Text = "Y: " + delta);
            mouse.On(Axis.Z).Do(delta => zLabel.Text = "Z: " + delta);

            form.Show();
            
            var timer = new Timer { Interval = 10 };
            timer.Tick += (s, e) =>
            {
                keyboard.Update();
                mouse.Update();
            };

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