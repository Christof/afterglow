namespace Afterglow.Input.SlimDX
{
    /// <summary>
    /// Base class for SlimDX input devices.
    /// </summary>
    public abstract class SlimDXInputDeviceBase : InputDeviceBase
    {
        /// <summary>
        /// Checks whether the pressed button was registered for an action and executes the action.
        /// </summary>
        /// <param name="button">The button.</param>
        protected void CheckPressedButton(Button button)
        {
            if (RegisteredButtons.ContainsKey(button))
            {
                var buttonAction = RegisteredButtons[button];
                buttonAction.WasDown = true;
                if (buttonAction.State == ButtonState.IsDown)
                {
                    buttonAction.ExecuteAction();
                }
            }
        }

        /// <summary>
        /// Checks whether the released button was pressed and was registered 
        /// for an action and executes the action.
        /// </summary>
        /// <param name="button">The button.</param>
        protected void CheckReleasedButton(Button button)
        {
            if (RegisteredButtons.ContainsKey(button))
            {
                var buttonAction = RegisteredButtons[button];
                if (buttonAction.State == ButtonState.WasPressed && buttonAction.WasDown)
                {
                    buttonAction.ExecuteAction();
                }
                buttonAction.WasDown = false;
            }
        }
    }
}