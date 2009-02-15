using System;
using TheNewEngine.Infrastructure;

namespace TheNewEngine.Input
{
    /// <summary>
    /// Action for an axis.s
    /// </summary>
    public class AxisAction : IAxisAction, IFluentInterface
    {
        private Action<int> mAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxisAction"/> class.
        /// </summary>
        /// <param name="axis">The axis.</param>
        public AxisAction(Axis axis)
        {
            Axis = axis;
        }

        /// <summary>
        /// Gets the axis.
        /// </summary>
        /// <value>The axis.</value>
        public Axis Axis { get; private set; }

        /// <summary>
        /// Sets the given action to be executed if the axis changes.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Do(Action<int> action)
        {
            mAction = action;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="delta">The delta value for this axis.</param>
        public void ExecuteAction(int delta)
        {
            mAction(delta);
        }
    }
}