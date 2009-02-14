using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;
using System;

namespace TheNewEngine.Input.SlimDX
{
    /// <summary>
    /// Keyboard implementation for SlimDX.
    /// </summary>
    public class SlimDXKeyboard : InputDeviceBase
    {
        private readonly Device<KeyboardState> mKeyboard;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXKeyboard"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public SlimDXKeyboard(Control control)
        {
            DirectInput.Initialize();

            mKeyboard = new Device<KeyboardState>(SystemGuid.Keyboard);
            mKeyboard.SetCooperativeLevel(control,
                CooperativeLevel.Foreground | CooperativeLevel.Exclusive | CooperativeLevel.NoWinKey);

            mKeyboard.Acquire();
        }

        /// <summary>
        /// Updates the input device.
        /// </summary>
        public override void Update()
        {
            if (mKeyboard.Acquire().IsFailure)
            {
                //MessageBox.Show("Failed to acquire the keyboard");
                return;
            }

            if (mKeyboard.Poll().IsFailure)
            {
                //MessageBox.Show("Failed to poll the keyboard");
                return;
            }

            KeyboardState state = mKeyboard.GetCurrentState();
            if (Result.Last.IsFailure)
            {
                return;
            }

            foreach (Key key in state.PressedKeys)
            {
                CheckPressedKey(key);
            }

            foreach (var key in state.ReleasedKeys)
            {
                CheckReleasedKey(key);
            }
        }

        private void CheckPressedKey(Key key)
        {
            var button = (Button) key;
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

        private void CheckReleasedKey(Key key)
        {
            var button = (Button)key;
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