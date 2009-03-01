using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Abstract implementation of <see cref="IGraphicStream"/>.
    /// </summary>
    /// <typeparam name="ElementType">The type of the element.</typeparam>
    [Serializable]
    public class GraphicStream<ElementType> : IGraphicStream, IEnumerable<ElementType>
        where ElementType : struct 
    {
        private readonly GraphicStreamDescription mDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicStream{ElementType}"/> class.
        /// </summary>
        /// <param name="usage">The usage.</param>
        /// <param name="data">The data which will be stored in the stream.</param>
        public GraphicStream(GraphicStreamUsage usage, IEnumerable<ElementType> data)
        {
            mDescription = new GraphicStreamDescription(usage,
                GraphicStreamFormatHelper.GetForTypeName(typeof (ElementType).Name),
                Marshal.SizeOf(typeof (ElementType)),
                data.Count());

            Data = data;
        }

        /// <summary>
        /// Gets the description of the graphic stream.
        /// </summary>
        /// <value>The description.</value>
        public GraphicStreamDescription Description
        {
            get { return mDescription; }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>Vertex or index data.</value>
        public IEnumerable<ElementType> Data { get; private set; }
        
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