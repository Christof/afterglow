using System.Collections.Generic;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;

namespace Afterglow.Input.SlimDX
{
    /// <summary>
    /// SlimDX mouse implementation.
    /// </summary>
    public class SlimDXMouse : InputDeviceBase, IAxesInputDevice
    {
        private readonly Device<MouseState> mMouse;

        private readonly Dictionary<Axis, AxisAction> mAxesActions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXMouse"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public SlimDXMouse(Control control)
        {
            mAxesActions = new Dictionary<Axis, AxisAction>(3);

            mMouse = new Device<MouseState>(SystemGuid.Mouse);
            mMouse.SetCooperativeLevel(control,
                CooperativeLevel.Foreground | CooperativeLevel.Exclusive);

            mMouse.Acquire();
        }

        /// <summary>
        /// Starts a configuration for an axis change.
        /// </summary>
        /// <param name="axis">The axis for which a configuration should be added.</param>
        /// <returns>An axis action.</returns>
        public IAxisAction On(Axis axis)
        {
            var action = new AxisAction(axis);
            mAxesActions.Add(axis, action);

            return action;
        }

        /// <summary>
        /// Updates the input device.
        /// </summary>
        public override void Update()
        {
            if (mMouse.Acquire().IsFailure)
            {
                //MessageBox.Show("Failed to acquire the keyboard");
                return;
            }

            if (mMouse.Poll().IsFailure)
            {
                //MessageBox.Show("Failed to poll the keyboard");
                return;
            }

            MouseState state = mMouse.GetCurrentState();
            if (Result.Last.IsFailure)
            {
                return;
            }

            if (state.X != 0)
            {
                mAxesActions[Axis.X].ExecuteAction(state.X);
            }

            if (state.Y != 0)
            {
                mAxesActions[Axis.Y].ExecuteAction(state.Y);
            }

            if (state.Z != 0)
            {
                mAxesActions[Axis.Z].ExecuteAction(state.Z);
            }

            var mouseOffset = (int) Button.LeftMouse;
            for (int i = 0; i < 8; i++)
            {
                var button = (Button)(mouseOffset + i);
                if (state.IsPressed(i))
                {
                    CheckDownButton(button);
                }
                if (state.IsReleased(i))
                {
                    CheckReleasedButton(button);
                }
            }
        }
    }
}