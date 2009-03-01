namespace Afterglow.Input
{
    /// <summary>
    /// Possible states of button on which an action can be triggered.
    /// </summary>
    public enum ButtonState
    {
        /// <summary>
        /// The button is pressed.
        /// </summary>
        IsDown,

        /// <summary>
        /// The button was down in the last frame and is now released
        /// </summary>
        WasPressed,
    }
}