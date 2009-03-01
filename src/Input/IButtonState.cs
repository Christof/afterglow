namespace Afterglow.Input
{
    /// <summary>
    /// Defines the state for a button action.
    /// </summary>
    public interface IButtonState
    {
        /// <summary>
        /// The action will be triggered if the button is pressed.
        /// </summary>
        /// <returns>A button action to define the action.</returns>
        IButtonAction IsDown();

        /// <summary>
        /// The action will be triggered if the button was pressed.
        /// </summary>
        /// <returns>A button action to define the action.</returns>
        IButtonAction WasPressed();
    }
}