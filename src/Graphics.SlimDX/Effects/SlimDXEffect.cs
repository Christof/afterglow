using SlimDX.Direct3D10;

namespace TheNewEngine.Graphics.Effects
{
    /// <summary>
    /// SlimDX effect implementation.
    /// </summary>
    public class SlimDXEffect : IEffect
    {
        internal Effect Effect { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXEffect"/> class.
        /// </summary>
        /// <param name="effect">The SlimDX effect.</param>
        public SlimDXEffect(Effect effect)
        {
            Effect = effect;
        }
    }
}