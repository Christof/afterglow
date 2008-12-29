namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Encapsulates the behavior to bind a (part of a) buffer.
    /// </summary>
    public abstract class BufferBinding
    {
        public GraphicStreamDescription Description { get; private set; }

        protected BufferBinding(GraphicStreamDescription description)
        {
            Description = description;
        }

        /// <summary>
        /// Binds the buffer.
        /// </summary>
        public abstract void Bind();
    }
}