using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;

namespace Afterglow.Input.SlimDX
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
                CheckDownButton((Button)key);
            }

            foreach (var key in state.ReleasedKeys)
            {
                CheckReleasedButton((Button)key);
            }
        }
    }
}