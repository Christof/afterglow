
namespace Afterglow.Graphics.Effects
{
    /// <summary>
    /// OpenTK effect implementation.
    /// </summary>
    public class OpenTKEffect : IEffect, System.IDisposable
    {
        internal int EffectHandle { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKEffect"/> class.
        /// </summary>
        /// <param name="effect">The open gl effect handle.</param>
        public OpenTKEffect(int effectHandle)
        {
            EffectHandle = effectHandle;
        }

        public void Dispose()
        {
            OpenTK.Graphics.GL.DeleteShader(EffectHandle);
        }
    }
}