using Microsoft.Xna.Framework.Input;

namespace Afterglow.Input.Xna
{
    /// <summary>
    /// Keyboard implementation with Xna.
    /// </summary>
    public class XnaKeyboard : InputDeviceBase
    {
        /// <summary>
        /// Updates the input device.
        /// </summary>
        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            foreach (Button button in RegisteredButtons.Keys)
            {
                Keys xnaKey = button.ToXna();
                if (state.IsKeyDown(xnaKey))
                {
                    RegisteredButtons[button].ExecuteAction();
                }
                if (state.IsKeyUp(xnaKey))
                {
                    CheckReleasedButton(button);
                }
            }
        }
    }
}