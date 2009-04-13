using System.Windows.Forms;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Graphics.Textures;
using Afterglow.Math;
using SlimDX.Direct3D10;

namespace Afterglow.Graphics
{
    /// <summary>
    /// SlimDX specific implementation of the api factory to provide all implementations
    /// of the abstract class or interfaces needed to use an api.
    /// </summary>
    public class SlimDXFactory : IApiFactory
    {
        private Device mDevice;

        /// <summary>
        /// Creates a new render window.
        /// </summary>
        /// <param name="control">The control in which the content will be rendered.</param>
        /// <returns>A new created render window.</returns>
        public IRenderWindow CreateRenderWindow(Control control)
        {
            var renderWindow = new SlimDXRenderWindow(control.Handle);

            mDevice = renderWindow.Device;

            return renderWindow;
        }

        /// <summary>
        /// Gets a buffer service to create buffer bindings.
        /// </summary>
        /// <returns>A buffer service.</returns>
        public IBufferService GetBufferService()
        {
            return new SlimDXBufferService(mDevice);
        }

        /// <summary>
        /// Creates a new object renderer with the given effect and buffer bindings.
        /// </summary>
        /// <param name="effect">The effect which should be applied.</param>
        /// <param name="bufferBindings">The buffer bindings which contain all vertex and index data.</param>
        /// <returns>A new object renderer.</returns>
        public IObjectRenderer CreateObjectRenderer(IEffect effect, BufferBinding[] bufferBindings)
        {
            return new SlimDXObjectRenderer(mDevice, effect, bufferBindings);
        }

        /// <summary>
        /// Creates a matrix effect parameter for the given semantic.
        /// </summary>
        /// <param name="semanticName">Name of the semantic.</param>
        /// <returns>A matrix effect parameter.</returns>
        public SemanticEffectParameter<Matrix> CreateMatrixParameter(string semanticName)
        {
            return new SlimDXMatrixEffectParameter(semanticName);
        }

        /// <summary>
        /// Creates a new texture from the given file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A new texture instance.</returns>
        public ITexture CreateTexture(string filename)
        {
            return new SlimDXTexture(filename, mDevice);
        }

        /// <summary>
        /// Creates a texture effect parameter.
        /// </summary>
        /// <param name="semanticName">Name of the semantic.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>A texture effect parameter.</returns>
        public SemanticEffectParameter<ITexture> CreateTextureParameter(string semanticName, ITexture texture)
        {
            return new SlimDXTextureEffectParameter(mDevice, semanticName, texture);
        }

        /// <summary>
        /// Gets the effect compiler.
        /// </summary>
        /// <returns>The effect compiler.</returns>
        public IEffectCompiler GetEffectCompiler()
        {
            return new SlimDXEffectCompiler(mDevice);
        }
    }
}