using System.Collections.Generic;

namespace Afterglow.Input
{
    /// <summary>
    /// Base class for input devices which stores the configuration for registered buttons.
    /// </summary>
    public abstract class InputDeviceBase : IInputDevice
    {
        /// <summary>
        /// Gets all registered buttons and their button action.
        /// </summary>
        /// <value>The registered buttons.</value>
        protected Dictionary<Button, ButtonAction> RegisteredButtons { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputDeviceBase"/> class.
        /// </summary>
        protected InputDeviceBase()
        {
            RegisteredButtons = new Dictionary<Button, ButtonAction>();
        }

        /// <summary>
        /// Starts a configuration for a button press.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>An button state.</returns>
        public IButtonState On(Button button)
        {
            var builder = new ButtonAction(button);
            RegisteredButtons.Add(button, builder);

            return builder;
        }

        /// <summary>
        /// Updates the input device.
        /// </summary>
        public abstract void Update();

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