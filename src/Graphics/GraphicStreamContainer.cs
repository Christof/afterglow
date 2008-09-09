using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Container for graphic streams.
    /// </summary>
    public class GraphicStreamContainer : FrameResourceDecorator, IEnumerable<IGraphicStream>
    {
        private FrameResourceContainer<IGraphicStream> mDecoree;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicStreamContainer"/> class.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        public GraphicStreamContainer(FrameResourceContainer<IGraphicStream> decoree)
            : base(decoree)
        {
            mDecoree = decoree;
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
            return mDecoree.GetEnumerator();
        }
    }
}