using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheNewEngine.Graphics.Effects
{
    /// <summary>
    /// Xna effect compiler.
    /// </summary>
    public class XnaEffectCompiler : IEffectCompiler
    {
        private readonly GraphicsDevice mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaEffectCompiler"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public XnaEffectCompiler(GraphicsDevice device)
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
            CompiledEffect compiledEffect = Effect.CompileEffectFromFile(
                path, null, null, CompilerOptions.Debug, TargetPlatform.Windows);

            if (compiledEffect.Success == false)
            {
                throw new Exception(compiledEffect.ErrorsAndWarnings);
            }

            var effect = new Effect(mDevice, compiledEffect.GetEffectCode(),
                CompilerOptions.Debug, new EffectPool());

            return new XnaEffect(effect);
        }
    }
}