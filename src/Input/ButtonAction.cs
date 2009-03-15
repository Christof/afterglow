using System;

namespace Afterglow.Input
{
    /// <summary>
    /// Action for a button and the information when the button is triggered.
    /// </summary>
    public class ButtonAction : IButtonState, IButtonAction
    {
        private Action mAction;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonAction"/> class.
        /// </summary>
        /// <param name="button">The button.</param>
        public ButtonAction(Button button)
        {
            Button = button;
        }

        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <value>The button.</value>
        public Button Button { get; private set; }

        /// <summary>
        /// Gets when the action should be triggered.
        /// </summary>
        /// <value>The state.</value>
        public ButtonState State { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the button was down in the last frame.
        /// This is need for triggering a was pressed action.
        /// (Maybe the slimdx implementation should derive from the button action and have this property).
        /// </summary>
        /// <value><c>true</c> if the button was down; otherwise, <c>false</c>.</value>
        // HACK let the implementation handle this.
        public bool WasDown { get; set; }

        /// <summary>
        /// The action will be triggered if the button is pressed.
        /// </summary>
        /// <returns>A button action to define the action.</returns>
        public IButtonAction IsDown()
        {
            State = ButtonState.IsDown;
            return this;
        }

        /// <summary>
        /// The action will be triggered if the button was released.
        /// </summary>
        /// <returns>A button action to define the action.</returns>
        public IButtonAction WasReleased()
        {
            State = ButtonState.WasReleased;
            return this;
        }

        /// <summary>
        /// The action will be triggered if the button was pressed.
        /// </summary>
        /// <returns>A button action to define the action.</returns>
        public IButtonAction WasPressed()
        {
            State = ButtonState.WasPressed;
            return this;
        }

        /// <summary>
        /// Sets the given action to be executed if the button is down or was pressed.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Do(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteAction()
        {
            mAction();
        }
    }
}