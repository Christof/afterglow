namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Buffer which contains vertex or index data.
    /// </summary>
    public interface IBuffer
    {
        /// <summary>
        /// Loads the specified graphic stream into the buffer.
        /// </summary>
        /// <param name="graphicStream">The graphic stream.</param>
        void Load<ElementType>(GraphicStream<ElementType> graphicStream)
            where ElementType : struct;

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        void Unload();
    }
}