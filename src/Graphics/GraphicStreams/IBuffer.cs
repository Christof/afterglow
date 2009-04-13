namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Storage for geometry data
    /// </summary>
    public interface IBuffer
    {
        /// <summary>
        /// Loads the specified graphic stream into the buffer.
        /// </summary>
        /// <typeparam name="ElementType">The type of the element.</typeparam>
        /// <param name="graphicStream">The graphic stream.</param>
        void Load<ElementType>(GraphicStream<ElementType> graphicStream)
            where ElementType : struct;

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        void Unload();

        /// <summary>
        /// Gets the description for the elements in the buffer.
        /// </summary>
        /// <value>The description.</value>
        GraphicStreamDescription Description { get; }
    }
}