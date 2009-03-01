using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Interface for Xna vertex buffers which must know the index of the buffer
    /// in the container to be able to set the buffer on the device.
    /// </summary>
    public interface IXnaVertexBuffer : IBuffer
    {
        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        VertexBuffer Buffer { get; }

        /// <summary>
        /// Gets the description for the elements in the buffer.
        /// </summary>
        /// <value>The description.</value>
        GraphicStreamDescription Description { get; }
    }
}