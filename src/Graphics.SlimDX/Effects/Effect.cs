using TheNewEngine.Graphics.Effects;
using SlimDXEffect = SlimDX.Direct3D10.Effect;

namespace TheNewEngine.Graphics.SlimDX.Effects
{
    /// <summary>
    /// SlimDX effect implementation.
    /// </summary>
    public class Effect : IEffect
    {
        internal SlimDXEffect SlimDXEffect { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXEffect"/> class.
        /// </summary>
        /// <param name="effect">The SlimDX effect.</param>
        public Effect(SlimDXEffect effect)
        {
            SlimDXEffect = effect;
        }
    }
}