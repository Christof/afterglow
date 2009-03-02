using System;
using System.Linq;
using Afterglow.Infrastructure;
using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Implementation of an index buffer with Xna.
    /// </summary>
    public class XnaIndexBuffer : IXnaBuffer
    {
        private readonly GraphicsDevice mDevice;

        private IndexBuffer mBuffer;

        private GraphicStreamDescription mDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaIndexBuffer"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public XnaIndexBuffer(GraphicsDevice device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Gets the index buffer.
        /// </summary>
        /// <value>The index buffer.</value>
        public IndexBuffer IndexBuffer
        {
            get { return mBuffer; }
        }

        /// <summary>
        /// Gets the vertex buffer.
        /// </summary>
        /// <value>The vertex buffer.</value>
        public VertexBuffer VertexBuffer
        {
            get { throw new InvalidOperationException("No VertexBuffer for a XnaIndexBuffer does exist!"); }
        }

        /// <summary>
        /// Gets the description for the elements in the buffer.
        /// </summary>
        /// <value>The description.</value>
        public GraphicStreamDescription Description
        {
            get { return mDescription; }
        }

        /// <summary>
        /// Loads the specified graphic stream into the buffer.
        /// </summary>
        /// <typeparam name="ElementType">The type of the element.</typeparam>
        /// <param name="graphicStream">The graphic stream.</param>
        public void Load<ElementType>(GraphicStream<ElementType> graphicStream) where ElementType : struct
        {
            mDescription = graphicStream.Description;

            mBuffer = new IndexBuffer(mDevice, typeof(ElementType), 
                mDescription.Count, BufferUsage.None);

            mBuffer.SetData(graphicStream.Data.ToArray());
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            mBuffer.DisposeIfNotNull();
        }
    }
}