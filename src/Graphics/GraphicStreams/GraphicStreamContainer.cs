using System.Collections;
using System.Collections.Generic;
using TheNewEngine.Graphics.Resources;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Container for graphic streams.
    /// </summary>
    public class GraphicStreamContainer : ResourceDecorator, IResourceContainer<IGraphicStream> 
    {
        private readonly List<IGraphicStream> mStreams = new List<IGraphicStream>();

        /// <summary>
        /// Adds the frame resource to the container.
        /// </summary>
        /// <param name="frameResource">The frame resource.</param>
        public void Add(IGraphicStream frameResource)
        {
            mStreams.Add(frameResource);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IGraphicStream> GetEnumerator()
        {
            return mStreams.GetEnumerator();
        }

        /// <summary>
        /// Creates a <see cref="GraphicStream{ElementType}"/> and adds it to the container.
        /// </summary>
        /// <typeparam name="ElementType">The type of the lement type.</typeparam>
        /// <param name="usage">The usage of the stream.</param>
        /// <param name="data">The data for the stream.</param>
        /// <returns>The create <see cref="GraphicStream{ElementType}"/>.</returns>
        public GraphicStream<ElementType> Create<ElementType>(GraphicStreamUsage usage, ElementType[] data)
            where ElementType : struct
        {
            var stream = new GraphicStream<ElementType>(usage, data);
            Add(stream);

            return stream;
        }
    }
}