using SlimDX.Direct3D10;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Creates SlimDX buffers for graphic streams.
    /// </summary>
    public class SlimDXBufferService : IBufferService
    {
        private readonly Device mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXBufferService"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public SlimDXBufferService(Device device)
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
            var buffer = new SlimDXBuffer(mDevice);
            buffer.Load(graphicStream);

            return new SlimDXBufferBinding(mDevice, buffer);
        }
    }
}