using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using XnaButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace Afterglow.Input.Xna
{
    /// <summary>
    /// Xna mouse implementation.
    /// </summary>
    public class XnaMouse : InputDeviceBase, IAxesInputDevice
    {
        private readonly Dictionary<Axis, AxisAction> mAxesActions;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaMouse"/> class.
        /// </summary>
        public XnaMouse()
        {
            //Mouse.WindowHandle = handle;
            mAxesActions = new Dictionary<Axis, AxisAction>(3);
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
            MouseState state = Mouse.GetState();

            if (state.X != 0)
            {
                mAxesActions[Axis.X].ExecuteAction(state.X);
            }

            if (state.Y != 0)
            {
                mAxesActions[Axis.Y].ExecuteAction(state.Y);
            }

            if (state.ScrollWheelValue != 0)
            {
                mAxesActions[Axis.Z].ExecuteAction(state.ScrollWheelValue);
            }

            if (state.LeftButton == XnaButtonState.Pressed)
            {
                CheckPressedButton(Button.LeftMouse);
            }
            if (state.MiddleButton == XnaButtonState.Pressed)
            {
                CheckPressedButton(Button.MiddleMouse);
            }
            if (state.RightButton == XnaButtonState.Pressed)
            {
                CheckPressedButton(Button.RightMouse);
            }
            if (state.XButton1 == XnaButtonState.Pressed)
            {
                CheckPressedButton(Button.Mouse4);
            }
            if (state.XButton2 == XnaButtonState.Pressed)
            {
                CheckPressedButton(Button.Mouse5);
            }

            if (state.LeftButton == XnaButtonState.Released)
            {
                CheckReleasedButton(Button.LeftMouse);
            }
            if (state.MiddleButton == XnaButtonState.Released)
            {
                CheckReleasedButton(Button.MiddleMouse);
            }
            if (state.RightButton == XnaButtonState.Released)
            {
                CheckReleasedButton(Button.RightMouse);
            }
            if (state.XButton1 == XnaButtonState.Released)
            {
                CheckReleasedButton(Button.Mouse4);
            }
            if (state.XButton2 == XnaButtonState.Released)
            {
                CheckReleasedButton(Button.Mouse5);
            }
        }
    }
}