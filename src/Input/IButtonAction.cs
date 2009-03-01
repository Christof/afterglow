using System;

namespace Afterglow.Input
{
    /// <summary>
    /// Defines the action of a button action.
    /// </summary>
    public interface IButtonAction
    {
        /// <summary>
        /// Sets the given action to be executed if the button is down or was pressed.
        /// </summary>
        /// <param name="action">The action.</param>
        void Do(Action action);
    }
}