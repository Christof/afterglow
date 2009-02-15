namespace TheNewEngine.Input
{
    /// <summary>
    /// Provides access to all input devices.
    /// </summary>
    public interface IInputDevices
    {
        /// <summary>
        /// Gets the keyboard.
        /// </summary>
        /// <value>The keyboard.</value>
        IInputDevice Keyboard { get; }

        /// <summary>
        /// Gets the mouse.
        /// </summary>
        /// <value>The mouse.</value>
        IInputDevice Mouse { get; }

        /// <summary>
        /// Updates the input devices.
        /// </summary>
        void Update();
    }
}