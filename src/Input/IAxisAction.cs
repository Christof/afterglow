using System;
namespace Afterglow.Input
{
    /// <summary>
    /// Defines the action of a axis action.
    /// </summary>
    public interface IAxisAction
    {
        /// <summary>
        /// Sets the given action to be executed if the axis changes.
        /// </summary>
        /// <param name="action">The action.</param>
        void Do(Action<int> action);
    }
}