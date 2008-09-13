using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Abstract implementation of <see cref="IGraphicStream"/>.
    /// </summary>
    /// <typeparam name="ElementType">The type of the lement type.</typeparam>
    public class GraphicStream<ElementType> : FrameResourceDecorator, IGraphicStream, IEnumerable<ElementType>
        where ElementType : struct 
    {
        private readonly GraphicStreamFormat mFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicStream{ElementType}"/> class.
        /// </summary>
        /// <param name="usage">The usage.</param>
        /// <param name="data">The data which will be stored in the stream.</param>
        public GraphicStream(GraphicStreamUsage usage, ElementType[] data)
        {
            Usage = usage;
            ElementSize = Marshal.SizeOf(typeof (ElementType));
            Data = data;
            Size = Data.Length * ElementSize;

            mFormat = GraphicStreamFormatHelper.GetForTypeName(typeof (ElementType).Name);
        }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <value>The usage.</value>
        public GraphicStreamUsage Usage { get; private set; }

        /// <summary>
        /// Gets the size of the element.
        /// </summary>
        /// <value>The size of the element.</value>
        public int ElementSize { get; private set; }

        /// <summary>
        /// Gets the overall size.
        /// </summary>
        /// <value>The overall size.</value>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>Vertex or index data.</value>
        public ElementType[] Data { get; private set; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        public GraphicStreamFormat Format
        {
            get
            {
                return mFormat;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<ElementType> GetEnumerator()
        {
            foreach (var element in Data)
            {
                yield return element;
            }
        }
    }
}