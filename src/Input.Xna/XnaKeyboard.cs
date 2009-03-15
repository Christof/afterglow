using Microsoft.Xna.Framework.Input;

namespace Afterglow.Input.Xna
{
    /// <summary>
    /// Keyboard implementation with Xna.
    /// </summary>
    public class XnaKeyboard : InputDeviceBase
    {
        private KeyboardState mLastState;

        /// <summary>
        /// Updates the input device.
        /// </summary>
        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            foreach (Button button in RegisteredButtons.Keys)
            {
                Keys xnaKey = button.ToXna();
                ButtonAction buttonAction = RegisteredButtons[button];

                switch (buttonAction.State)
                {
                    case ButtonState.IsDown:
                        if (state.IsKeyDown(xnaKey))
                        {
                            buttonAction.ExecuteAction();
                        }
                        break;

                    case ButtonState.WasPressed:
                        if (state.IsKeyDown(xnaKey) && mLastState.IsKeyUp(xnaKey))
                        {
                            buttonAction.ExecuteAction();
                        }
                        break;

                    case ButtonState.WasReleased:
                        if (state.IsKeyUp(xnaKey) && mLastState.IsKeyDown(xnaKey))
                        {
                            buttonAction.ExecuteAction();
                        }
                        break;
                }

                mLastState = state;
            }
        }
    }
}