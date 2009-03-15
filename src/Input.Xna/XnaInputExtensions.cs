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

                case Button.Equals:
                    return Keys.None;

                case Button.Escape:
                    return Keys.Escape;

                case Button.F:
                    return Keys.F;

                case Button.F1:
                    return Keys.F1;

                case Button.F2:
                    return Keys.F2;

                case Button.F3:
                    return Keys.F3;

                case Button.F4:
                    return Keys.F4;

                case Button.F5:
                    return Keys.F5;

                case Button.F6:
                    return Keys.F6;

                case Button.F7:
                    return Keys.F7;

                case Button.F8:
                    return Keys.F8;

                case Button.F9:
                    return Keys.F9;

                case Button.F10:
                    return Keys.F10;

                case Button.F11:
                    return Keys.F11;

                case Button.F12:
                    return Keys.F12;

                case Button.F13:
                    return Keys.F13;

                case Button.F14:
                    return Keys.F14;

                case Button.F15:
                    return Keys.F15;

                case Button.G:
                    return Keys.G;

                case Button.Grave:
                    return Keys.None;

                case Button.H:
                    return Keys.H;

                case Button.Home:
                    return Keys.Home;

                case Button.I:
                    return Keys.I;

                case Button.Insert:
                    return Keys.Insert;

                case Button.J:
                    return Keys.J;

                case Button.K:
                    return Keys.K;

                case Button.Kana:
                    return Keys.None;

                case Button.Kanji:
                    return Keys.None;

                case Button.L:
                    return Keys.L;

                case Button.LeftAlt:
                    return Keys.LeftAlt;

                case Button.LeftArrow:
                    return Keys.Left;

                case Button.LeftBracket:
                    return Keys.OemOpenBrackets;

                case Button.LeftControl:
                    return Keys.LeftControl;

                case Button.LeftShift:
                    return Keys.LeftShift;

                case Button.LeftWindowsKey:
                    return Keys.LeftWindows;

                case Button.M:
                    return Keys.M;

                case Button.Mail:
                    return Keys.LaunchMail;

                case Button.MediaSelect:
                    return Keys.SelectMedia;

                case Button.MediaStop:
                    return Keys.MediaStop;

                case Button.Minus:
                    return Keys.OemMinus;

                case Button.Mute:
                    return Keys.VolumeMute;

                case Button.MyComputer:
                    return Keys.None;

                case Button.N:
                    return Keys.N;

                case Button.NextTrack:
                    return Keys.MediaNextTrack;

                case Button.NoConvert:
                    return Keys.None;

                case Button.NumberLock:
                    return Keys.NumLock;

                case Button.NumberPad0:
                    return Keys.NumPad0;

                case Button.NumberPad1:
                    return Keys.NumPad1;

                case Button.NumberPad2:
                    return Keys.NumPad2;

                case Button.NumberPad3:
                    return Keys.NumPad3;

                case Button.NumberPad4:
                    return Keys.NumPad4;

                case Button.NumberPad5:
                    return Keys.NumPad5;

                case Button.NumberPad6:
                    return Keys.NumPad6;

                case Button.NumberPad7:
                    return Keys.NumPad7;

                case Button.NumberPad8:
                    return Keys.NumPad8;

                case Button.NumberPad9:
                    return Keys.NumPad9;

                case Button.NumberPadComma:
                    return Keys.Decimal;

                case Button.NumberPadEnter:
                    return Keys.Enter;

                case Button.NumberPadEquals:
                    return Keys.None;

                case Button.NumberPadMinus:
                    return Keys.Subtract;

                case Button.NumberPadPeriod:
                    return Keys.None;

                case Button.NumberPadPlus:
                    return Keys.Add;

                case Button.NumberPadSlash:
                    return Keys.Divide;

                case Button.NumberPadStar:
                    return Keys.Multiply;

                case Button.O:
                    return Keys.O;

                case Button.Oem102:
                    return Keys.None;

                case Button.P:
                    return Keys.P;

                case Button.PageDown:
                    return Keys.PageDown;

                case Button.PageUp:
                    return Keys.PageUp;

                case Button.Pause:
                    return Keys.Pause;

                case Button.Period:
                    return Keys.OemPeriod;

                case Button.PlayPause:
                    return Keys.Pause; // NOTE Not sure if this mapping is correct.

                case Button.Power:
                    return Keys.None;

                case Button.PreviousTrack:
                    return Keys.MediaPreviousTrack;

                case Button.PrintScreen:
                    return Keys.PrintScreen;

                case Button.Q:
                    return Keys.Q;

                case Button.R:
                    return Keys.R;

                case Button.Return:
                    return Keys.Enter;

                case Button.RightAlt:
                    return Keys.RightAlt;

                case Button.RightArrow:
                    return Keys.Right;

                case Button.RightBracket:
                    return Keys.OemCloseBrackets;

                case Button.RightControl:
                    return Keys.RightControl;

                case Button.RightShift:
                    return Keys.RightShift;

                case Button.RightWindowsKey:
                    return Keys.RightWindows;

                case Button.S:
                    return Keys.S;

                case Button.ScrollLock:
                    return Keys.Scroll;

                case Button.Semicolon:
                    return Keys.OemSemicolon;

                case Button.Slash:
                    return Keys.None;

                case Button.Sleep:
                    return Keys.Sleep;

                case Button.Space:
                    return Keys.Space;

                case Button.Stop:
                    return Keys.MediaStop;

                case Button.T:
                    return Keys.T;

                case Button.Tab:
                    return Keys.Tab;

                case Button.U:
                    return Keys.U;

                case Button.Underline:
                    return Keys.None;

                case Button.Unknown:
                    return Keys.None;

                case Button.Unlabeled:
                    return Keys.None;

                case Button.UpArrow:
                    return Keys.Up;

                case Button.V:
                    return Keys.V;

                case Button.VolumeDown:
                    return Keys.VolumeDown;

                case Button.VolumeUp:
                    return Keys.VolumeUp;

                case Button.W:
                    return Keys.W;

                case Button.Wake:
                    return Keys.None;

                case Button.WebBack:
                    return Keys.BrowserBack;

                case Button.WebFavorites:
                    return Keys.BrowserFavorites;

                case Button.WebForward:
                    return Keys.BrowserForward;

                case Button.WebHome:
                    return Keys.BrowserHome;

                case Button.WebRefresh:
                    return Keys.BrowserRefresh;

                case Button.WebSearch:
                    return Keys.BrowserSearch;

                case Button.WebStop:
                    return Keys.BrowserStop;

                case Button.X:
                    return Keys.X;

                case Button.Y:
                    return Keys.Y;

                case Button.Yen:
                    return Keys.None;

                case Button.Z:
                    return Keys.Z;

                default:
                    return Keys.None;
            }
        }
    }
}