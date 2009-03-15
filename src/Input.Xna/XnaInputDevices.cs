namespace Afterglow.Input.Xna
{
    /// <summary>
    /// Implementation of <see cref="IInputDevices"/> for Xna.
    /// </summary>
    public class XnaInputDevices : IInputDevices
    {
        private readonly XnaKeyboard mKeyboard;

        private readonly XnaMouse mMouse;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaInputDevices"/> class.
        /// </summary>
        public XnaInputDevices()
        {
            mKeyboard = new XnaKeyboard();
            mMouse = new XnaMouse();
        }

        /// <summary>
        /// Gets the keyboard.
        /// </summary>
        /// <value>The keyboard.</value>
        public IInputDevice Keyboard
        {
            get { return mKeyboard; }
        }

        /// <summary>
        /// Gets the mouse.
        /// </summary>
        /// <value>The mouse.</value>
        public IAxesInputDevice Mouse
        {
            get { return mMouse; }
        }

        /// <summary>
        /// Updates the input devices.
        /// </summary>
        public void Update()
        {
            mKeyboard.Update();
            mMouse.Update();
        }
    }
}