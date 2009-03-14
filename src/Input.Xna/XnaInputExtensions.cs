using Microsoft.Xna.Framework.Input;

namespace Afterglow.Input.Xna
{
    /// <summary>
    /// Contains extensions methods specific to Xna input.
    /// </summary>
    public static class XnaInputExtensions
    {
        /// <summary>
        /// Gets the corresponding <see cref="Keys"/> of the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The corresponding Xna key.</returns>
        public static Keys ToXna(this Button button)
        {
            switch (button)
            {
                case Button.A:
                    return Keys.A;

                case Button.AbntC1:
                    return Keys.None;

                case Button.AbntC2:
                    return Keys.None;

                case Button.Apostrophe:
                    return Keys.None;

                case Button.Applications:
                    return Keys.Apps;

                case Button.AT:
                    return Keys.None;

                case Button.AX:
                    return Keys.None;

                case Button.B:
                    return Keys.B;

                case Button.Backslash:
                    return Keys.OemBackslash;

                case Button.Backspace:
                    return Keys.Back;

                case Button.C:
                    return Keys.C;

                case Button.Calculator:
                    return Keys.None;

                case Button.CapsLock:
                    return Keys.CapsLock;

                case Button.Colon:
                    return Keys.None;
                    
                case Button.Comma:
                    return Keys.OemComma;

                case Button.Convert:
                    return Keys.None;

                case Button.D:
                    return Keys.D;

                case Button.D0:
                    return Keys.D0;

                case Button.D1:
                    return Keys.D1;

                case Button.D2:
                    return Keys.D2;

                case Button.D3:
                    return Keys.D3;

                case Button.D4:
                    return Keys.D4;

                case Button.D5:
                    return Keys.D5;

                case Button.D6:
                    return Keys.D6;

                case Button.D7:
                    return Keys.D7;

                case Button.D8:
                    return Keys.D8;

                case Button.D9:
                    return Keys.D9;

                case Button.Delete:
                    return Keys.Delete;

                case Button.DownArrow:
                    return Keys.Down;

                case Button.E:
                    return Keys.E;

                case Button.End:
                    return Keys.End;


                case Button.S:
                    return Keys.S;

                case Button.W:
                    return Keys.W;

                case Button.X:
                    return Keys.X;

                case Button.Escape:
                    return Keys.Escape;

                default:
                    return Keys.None;
            }
        }
    }
}