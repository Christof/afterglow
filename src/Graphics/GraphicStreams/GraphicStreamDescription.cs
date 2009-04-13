using System;
namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Info about a graphic stream.
    /// </summary>
    [Serializable]
    public struct GraphicStreamDescription
    {
        private readonly GraphicStreamUsage mUsage;

        private readonly GraphicStreamFormat mFormat;

        private readonly int mElementSizeInBytes;

        private readonly int mCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicStreamDescription"/> class.
        /// </summary>
        /// <param name="usage">The usage.</param>
        /// <param name="format">The format.</param>
        /// <param name="elementSizeInBytes">Element size in bytes.</param>
        /// <param name="count">The count.</param>
        public GraphicStreamDescription(GraphicStreamUsage usage, 
            GraphicStreamFormat format, int elementSizeInBytes, int count)
        {
            mUsage = usage;
            mFormat = format;
            mElementSizeInBytes = elementSizeInBytes;
            mCount = count;
        }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <value>The usage.</value>
        public GraphicStreamUsage Usage
        {
            get { return mUsage; }
        }

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        public GraphicStreamFormat Format
        {
            get { return mFormat; }
        }

        /// <summary>
        /// Gets the size of one element in bytes.
        /// </summary>
        /// <value>The size of one element.</value>
        public int ElementSizeInBytes
        {
            get { return mElementSizeInBytes; }
        }

        /// <summary>
        /// Gets the count of elements.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return mCount; }
        }

        /// <summary>
        /// Gets overall size in bytes.
        /// </summary>
        /// <value>The overall size.</value>
        public int TotalSizeInBytes
        {
            get
            {
                return Count * ElementSizeInBytes;
            }
        }
    }
}