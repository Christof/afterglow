using System;
using System.Windows.Forms;

namespace TheNewEngine.Input.SlimDX
{
    /// <summary>
    /// SlimDX input devices.
    /// </summary>
    public class SlimDXInputDevices : IInputDevices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXInputDevices"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public SlimDXInputDevices(Control control)
        {
            Keyboard = new SlimDXKeyboard(control);
        }

        /// <summary>
        /// Gets the keyboard.
        /// </summary>
        /// <value>The keyboard.</value>
        public IInputDevice Keyboard { get; private set; }

        /// <summary>
        /// Updates the input devices.
        /// </summary>
        public void Update()
        {
            Keyboard.Update();
        }
    }
}