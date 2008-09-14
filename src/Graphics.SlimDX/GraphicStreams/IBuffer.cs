using TheNewEngine.Graphics.Resources;

namespace TheNewEngine.Graphics.SlimDX.GraphicStreams
{
    /// <summary>
    /// Interface for SlimDX buffers which must know the index of the buffer
    /// in the container to be able to set the buffer on the input assembler.
    /// </summary>
    public interface IBuffer : IFrameResource
    {
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        int Index { get; set; }
    }
}