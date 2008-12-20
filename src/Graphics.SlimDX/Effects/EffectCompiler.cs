using TheNewEngine.Graphics.Effects;
using SlimDX.Direct3D10;
using SlimDXEffect = SlimDX.Direct3D10.Effect;

namespace TheNewEngine.Graphics.SlimDX.Effects
{
    /// <summary>
    /// Implementation of an effect compiler for SlimDX.
    /// </summary>
    public class EffectCompiler : IEffectCompiler
    {
        private Device mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectCompiler"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public EffectCompiler(Device device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Compiles an effect file specified trough the given path.
        /// </summary>
        /// <param name="path">The path to the effect file.</param>
        /// <returns>The compiled effect.</returns>
        public IEffect Compile(string path)
        {
            string errors;
            var effect = SlimDXEffect.FromFile(
                mDevice, path, "fx_4_0",
                ShaderFlags.Debug, EffectFlags.None, 
                null, null, out errors);

            return new Effect(effect);
        }
    }
}