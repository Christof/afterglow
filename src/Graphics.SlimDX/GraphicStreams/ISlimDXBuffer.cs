using SlimDX.Direct3D10;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Interface for SlimDX buffers which must know the index of the buffer
    /// in the container to be able to set the buffer on the input assembler.
    /// </summary>
    public interface ISlimDXBuffer : IBuffer
    {
        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        Buffer Buffer { get; }

        /// <summary>
        /// Gets the description for the elements in the buffer.
        /// </summary>
        /// <value>The description.</value>
        GraphicStreamDescription Description { get; }
    }
}