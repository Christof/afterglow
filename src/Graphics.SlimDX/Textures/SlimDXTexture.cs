using SlimDX.Direct3D10;

namespace Afterglow.Graphics.Textures
{
    /// <summary>
    /// SlimDX texture implementation.
    /// </summary>
    public class SlimDXTexture : ITexture
    {
        private readonly string mFilename;

        private readonly Device mDevice;

        internal Texture2D Texture { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXTexture"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="device">The device.</param>
        public SlimDXTexture(string filename, Device device)
        {
            mFilename = filename;
            mDevice = device;
        }
        
        /// <summary>
        /// Loads the resource.
        /// </summary>
        public void Load()
        {
            Texture = Texture2D.FromFile(mDevice, mFilename);
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            Texture.DisposeIfNotDisposed();
        }
    }
}