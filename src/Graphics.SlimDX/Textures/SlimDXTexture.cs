using SlimDX.Direct3D10;
using TheNewEngine.Graphics.Resources;

namespace TheNewEngine.Graphics.Textures
{
    /// <summary>
    /// SlimDX texture implementation.
    /// </summary>
    public class SlimDXTexture : IResource
    {
        private readonly Device mDevice;

        private Texture2D mTexture;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXTexture"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public SlimDXTexture(Device device)
        {
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
        /// <param name="decoree">The decoree.</param>
        public void Load(IResource decoree)
        {
            mTexture = Texture2D.FromFile(mDevice, ((NamedResourceDecorator)decoree).Name);
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            if (mTexture != null && !mTexture.Disposed)
            {
                mTexture.Dispose();
                mTexture = null;
            }
        }
    }
}