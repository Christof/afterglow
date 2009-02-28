using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using TheNewEngine.Infrastructure;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Implementation of a buffer with Xna.
    /// </summary>
    public class XnaVertexBuffer : IXnaVertexBuffer
    {
        private readonly GraphicsDevice mDevice;

        private VertexBuffer mBuffer;

        private GraphicStreamDescription mDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaVertexBuffer"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public XnaVertexBuffer(GraphicsDevice device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        public VertexBuffer Buffer
        {
            get { return mBuffer; }
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

            mBuffer = new VertexBuffer(mDevice, typeof(ElementType), 
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