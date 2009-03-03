namespace Afterglow.Graphics.Textures
{
    /// <summary>
    /// Texture interface.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Loads the texture;
        /// </summary>
        void Load();

        /// <summary>
        /// Unloads the texture.
        /// </summary>
        void Unload();
    }
}