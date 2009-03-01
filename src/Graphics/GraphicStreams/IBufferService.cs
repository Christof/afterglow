namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Provides functions to store graphic streams in buffers.
    /// </summary>
    public interface IBufferService
    {
        /// <summary>
        /// Creates a buffer for the given graphic stream.
        /// </summary>
        /// <typeparam name="ElementType">The type of each element in the graphic stream.</typeparam>
        /// <param name="graphicStream">The graphic stream.</param>
        /// <returns>A buffer binding to enable the buffer each frame.</returns>
        BufferBinding CreateFor<ElementType>(GraphicStream<ElementType> graphicStream)
            where ElementType : struct;
    }
}