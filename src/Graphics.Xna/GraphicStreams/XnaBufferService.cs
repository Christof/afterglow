using Microsoft.Xna.Framework.Graphics;
namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Implementaiton of a <see cref="IBufferService"/> for Xna.
    /// </summary>
    public class XnaBufferService : IBufferService
    {
        private readonly GraphicsDevice mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaBufferService"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public XnaBufferService(GraphicsDevice device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Creates a buffer for the given graphic stream.
        /// </summary>
        /// <typeparam name="ElementType">The type of each element in the graphic stream.</typeparam>
        /// <param name="graphicStream">The graphic stream.</param>
        /// <returns>A buffer binding to enable the buffer each frame.</returns>
        public BufferBinding CreateFor<ElementType>(GraphicStream<ElementType> graphicStream)
            where ElementType : struct
        {
            IXnaBuffer buffer;
            if (graphicStream.Description.Usage == GraphicStreamUsage.Index)
            {
                buffer = new XnaIndexBuffer(mDevice);
            }
            else
            {
                buffer = new XnaVertexBuffer(mDevice);
            }
            buffer.Load(graphicStream);

            return new XnaBufferBinding(mDevice, buffer);
        }
    }
}