using Afterglow.Infrastructure;
using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.Textures
{
    /// <summary>
    /// Xna texture implementation.
    /// </summary>
    public class XnaTexture : ITexture
    {
        private readonly string mFilename;

        private readonly GraphicsDevice mDevice;

        internal Texture2D Texture { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTexture"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="device">The device.</param>
        public XnaTexture(string filename, GraphicsDevice device)
        {
            mFilename = filename;
            mDevice = device;
        }

        /// <summary>
        /// Loads the texture;
        /// </summary>
        public void Load()
        {
            Texture = Texture2D.FromFile(mDevice, mFilename);
        }

        /// <summary>
        /// Unloads the texture.
        /// </summary>
        public void Unload()
        {
            Texture.DisposeIfNotNull();
        }
    }
}