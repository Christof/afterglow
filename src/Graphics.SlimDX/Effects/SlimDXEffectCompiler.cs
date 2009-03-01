using SlimDX.Direct3D10;

namespace Afterglow.Graphics.Effects
{
    /// <summary>
    /// Implementation of an effect compiler for SlimDX.
    /// </summary>
    public class SlimDXEffectCompiler : IEffectCompiler
    {
        private readonly Device mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXEffectCompiler"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public SlimDXEffectCompiler(Device device)
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
            var effect = Effect.FromFile(
                mDevice, path, "fx_4_0",
                ShaderFlags.Debug, EffectFlags.None, 
                null, null, out errors);

            return new SlimDXEffect(effect);
        }
    }
}