using System.Collections;
using System.Collections.Generic;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Container for graphic streams.
    /// </summary>
    public class GraphicStreamContainer : FrameResourceDecorator, IFrameResourceContainer<IGraphicStream> 
    {
        private List<IGraphicStream> mStreams = new List<IGraphicStream>();

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

        public void Create<ElementType>(GraphicStreamUsage usage, ElementType[] data)
            where ElementType : struct
        {
            var stream = new GraphicStream<ElementType>(usage, data);
            Add(stream);
        }
    }
}