using System.Collections;
using TheNewEngine.Graphics.Resources;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Interface for graphic streams which contain data for an element of a vertex,
    /// like positions, colors, normals or texture coordinates.
    /// </summary>
    public interface IGraphicStream : IFrameResource, IEnumerable
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

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        GraphicStreamFormat Format { get; }
    }
}