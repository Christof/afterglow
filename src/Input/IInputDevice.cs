namespace TheNewEngine.Input
{
    /// <summary>
    /// Interface for all input devices.
    /// </summary>
    public interface IInputDevice
    {
        /// <summary>
        /// Starts a configuration for a button press.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>An button state.</returns>
        IButtonState On(Button button);

        /// <summary>
        /// Updates the input device.
        /// </summary>
        void Update();
    }
}