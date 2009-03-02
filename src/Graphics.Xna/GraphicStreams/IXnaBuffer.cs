using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Interface for Xna vertex and index buffers which must know the index of the buffer
    /// in the container to be able to set the buffer on the device.
    /// </summary>
    public interface IXnaBuffer : IBuffer
    {
        /// <summary>
        /// Gets the vertex buffer.
        /// </summary>
        /// <value>The vertex buffer.</value>
        VertexBuffer VertexBuffer { get; }

        /// <summary>
        /// Gets the index buffer.
        /// </summary>
        /// <value>The index buffer.</value>
        IndexBuffer IndexBuffer { get; }

        /// <summary>
        /// Gets the description for the elements in the buffer.
        /// </summary>
        /// <value>The description.</value>
        GraphicStreamDescription Description { get; }
    }
}