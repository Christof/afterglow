using Microsoft.Xna.Framework.Graphics;

namespace TheNewEngine.Graphics.Effects
{
    /// <summary>
    /// Xna effect implementation.
    /// </summary>
    public class XnaEffect : IEffect
    {
        internal Effect Effect { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaEffect"/> class.
        /// </summary>
        /// <param name="effect">The SlimDX effect.</param>
        public XnaEffect(Effect effect)
        {
            Effect = effect;
        }
    }
}