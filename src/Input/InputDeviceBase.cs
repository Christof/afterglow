using System.Collections.Generic;

namespace TheNewEngine.Input
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
    }
}