using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;

namespace TheNewEngine.Input.SlimDX
{
    /// <summary>
    /// SlimDX mouse implementation.
    /// </summary>
    public class SlimDXMouse : SlimDXInputDeviceBase
    {
        private readonly Device<MouseState> mMouse;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXMouse"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public SlimDXMouse(Control control)
        {
            mMouse = new Device<MouseState>(SystemGuid.Mouse);
            mMouse.SetCooperativeLevel(control,
                CooperativeLevel.Foreground | CooperativeLevel.Exclusive);

            mMouse.Acquire();
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
                return;

            const int mouseOffset = (int) Button.LeftMouse;
            for (int i = 0; i < 8; i++)
            {
                var button = (Button) (mouseOffset + i);
                if (state.IsPressed(i))
                {
                    CheckPressedButton(button);
                }
                if (state.IsReleased(i))
                {
                    CheckReleasedButton(button);
                }
            }
        }
    }
}