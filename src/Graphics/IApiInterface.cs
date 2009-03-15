
using System.Windows.Forms;

using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Math;
using Afterglow.Graphics.Textures;

namespace Afterglow.Graphics
{
    /// <summary>
    /// Provides factory methods for api abstractions which musst be implemented by
    /// each api.
    /// </summary>
    public interface IApiFactory
    {
        /// <summary>
        /// Creates a new render window.
        /// </summary>
        /// <param name="control">The control in which the content will be rendered.</param>
        /// <returns>A new created render window.</returns>
        IRenderWindow CreateRenderWindow(Control control);

        /// <summary>
        /// Gets a buffer service to create buffer bindings.
        /// </summary>
        /// <returns>A buffer service.</returns>
        IBufferService GetBufferService();

        /// <summary>
        /// Creates a new object renderer with the given effect and buffer bindings.
        /// </summary>
        /// <param name="effect">The effect which should be applied.</param>
        /// <param name="bufferBindings">The buffer bindings which contain all vertex and index data.</param>
        /// <returns>A new object renderer.</returns>
        IObjectRenderer CreateObjectRenderer(IEffect effect, BufferBinding[] bufferBindings);

        /// <summary>
        /// Creates a new texture from the given file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A new texture instance.</returns>
        ITexture CreateTexture(string filename);

        /// <summary>
        /// Creates a matrix effect parameter for the given semantic.
        /// </summary>
        /// <param name="semanticName">Name of the semantic.</param>
        /// <returns>A matrix effect parameter.</returns>
        SemanticEffectParameter<Matrix> CreateMatrixParameter(string semanticName);

        /// <summary>
        /// Creates a texture effect parameter.
        /// </summary>
        /// <param name="semanticName">Name of the semantic.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>A texture effect parameter.</returns>
        SemanticEffectParameter<ITexture> CreateTextureParameter(string semanticName, ITexture texture);

        /// <summary>
        /// Gets the effect compiler.
        /// </summary>
        /// <returns>The effect compiler.</returns>
        IEffectCompiler GetEffectCompiler();
    }
}