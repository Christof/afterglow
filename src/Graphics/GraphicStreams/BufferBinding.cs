namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Encapsulates the behavior to bind a (part of a) buffer.
    /// </summary>
    public abstract class BufferBinding
    {
        /// <summary>
        /// Gets the description of the elements in the referenced buffer.
        /// </summary>
        /// <value>The description.</value>
        public GraphicStreamDescription Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferBinding"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
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