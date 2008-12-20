using TheNewEngine.Graphics.Effects;
using SlimDXEffect = SlimDX.Direct3D10.Effect;

namespace TheNewEngine.Graphics.SlimDX.Effects
{
    /// <summary>
    /// SlimDX effect implementation.
    /// </summary>
    public class Effect : IEffect
    {
        private SlimDXEffect mEffect;

        /// <summary>
        /// Initializes a new instance of the <see cref="Effect"/> class.
        /// </summary>
        /// <param name="effect">The SlimDX effect.</param>
        public Effect(SlimDXEffect effect)
        {
            mEffect = effect;
        }
    }
}