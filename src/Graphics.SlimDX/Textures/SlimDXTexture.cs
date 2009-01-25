using SlimDX.Direct3D10;

namespace TheNewEngine.Graphics.Textures
{
    /// <summary>
    /// SlimDX texture implementation.
    /// </summary>
    public class SlimDXTexture
    {
        private readonly string mFilename;

        private readonly Device mDevice;

        private Texture2D mTexture;

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
        /// Called each frame before rendering so the the resource could be updated.
        /// </summary>
        public void OnFrame()
        {
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        public void Load()
        {
            mTexture = Texture2D.FromFile(mDevice, mFilename);
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            mTexture.DisposeIfNotDisposed();
        }
    }
}