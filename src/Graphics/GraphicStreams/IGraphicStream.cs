using System.Collections;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Interface for graphic streams which contain data for an element of a vertex,
    /// like positions, colors, normals or texture coordinates.
    /// </summary>
    public interface IGraphicStream : IEnumerable
    {
        /// <summary>
        /// Gets the description of the graphic stream.
        /// </summary>
        /// <value>The description.</value>
        GraphicStreamDescription Description { get; }
    }
}