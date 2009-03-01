namespace Afterglow.Graphics.Effects
{
    /// <summary>
    /// Effect compiler.
    /// </summary>
    public interface IEffectCompiler
    {
        /// <summary>
        /// Compiles an effect file specified trough the given path.
        /// </summary>
        /// <param name="path">The path to the effect file.</param>
        /// <returns>The compiled effect.</returns>
        IEffect Compile(string path);
    }
}