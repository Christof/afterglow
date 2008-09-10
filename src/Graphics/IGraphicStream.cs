namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Interface for graphic streams which contain data for an element of a vertex,
    /// like positions, colors, normals or texture coordinates.
    /// </summary>
    public interface IGraphicStream : IFrameResource
    {
        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <value>The usage.</value>
        GraphicStreamUsage Usage { get; }

        /// <summary>
        /// Gets the size of the element.
        /// </summary>
        /// <value>The size of the element.</value>
        int ElementSize { get; }

        /// <summary>
        /// Gets the overall size.
        /// </summary>
        /// <value>The overall size.</value>
        int Size { get; }
    }
}