using System;

namespace Afterglow.Infrastructure
{
    /// <summary>
    /// Provides a method to get an action.
    /// </summary>
    public static class The
    {
        /// <summary>
        /// Convenience method to get an action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The action to use for expectationss.</returns>
        public static Action Action(Action action)
        {
            return action;
        }
    }
}