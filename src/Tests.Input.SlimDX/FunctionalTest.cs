using System.Windows.Forms;
using MbUnit.Framework;

namespace TheNewEngine.Input.SlimDX
{
    public class FunctionalTest
    {
        [Test]
        public void Test()
        {
            var form = new Form();

            var close = false;
            var inputDevices = new SlimDXInputDevices(form);
            var keyboard = inputDevices.Keyboard;
            keyboard.On(Button.X).IsDown().Do(() => close = true);
            keyboard.On(Button.C).WasPressed().Do(() => close = true);
            keyboard.On(Button.W).WasPressed().Do(() => form.Text = "Pressed");
            keyboard.On(Button.S).IsDown().Do(() => form.Text = "Down");

            form.Show();
            
            var timer = new Timer {Interval = 10};
            timer.Tick += (s, e) => keyboard.Update();

            timer.Start();

            while (!close)
            {
                Application.DoEvents();
            }
        }
    }
}