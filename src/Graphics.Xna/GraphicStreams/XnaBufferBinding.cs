using Microsoft.Xna.Framework.Graphics;
namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Binds a <see cref="IXnaBuffer"/> to the device.
    /// </summary>
    public class XnaBufferBinding : BufferBinding
    {
        private readonly GraphicsDevice mDevice;

        private readonly IXnaBuffer mBuffer;

        private int mSlot = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaBufferBinding"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="buffer">The buffer.</param>
        public XnaBufferBinding(GraphicsDevice device, IXnaBuffer buffer)
            : base(buffer.Description)
        {
            mDevice = device;
            mBuffer = buffer;
        }

        /// <summary>
        /// Binds the buffer.
        /// </summary>
        public override void Bind()
        {
            if (Description.Usage == GraphicStreamUsage.Index)
            {
                mDevice.Indices = mBuffer.IndexBuffer;
            }
            else
            {
                mDevice.Vertices[mSlot].SetSource(
                    mBuffer.VertexBuffer, 0, mBuffer.Description.ElementSizeInBytes);
            }
        }
        
        /// <summary>
        /// Creates the vertex element using the description.
        /// </summary>
        /// <remarks>
        /// This method sets the slot for the input assembler.
        /// It must be called before the first call to <see cref="Bind"/>.
        /// </remarks>
        /// <param name="slot">The input slot for binding.</param>
        /// <returns>An input element.</returns>
        internal VertexElement CreateInputElement(int slot)
        {
            mSlot = slot;

            return new VertexElement((short)slot, 0, 
                mBuffer.Description.Format.ToFormat(),
                VertexElementMethod.Default, 
                mBuffer.Description.Usage.ToVertexElementUsage(), 0);
        }
    }
}