using System.Linq;
using SlimDX.Direct3D10;
using SlimDX;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Implementation of a buffer with SlimDX.
    /// </summary>
    public class SlimDXBuffer : ISlimDXBuffer
    {
        private readonly Device mDevice;

        private Buffer mBuffer;

        private GraphicStreamDescription mDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXBuffer"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public SlimDXBuffer(Device device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        public Buffer Buffer
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
        public void Load<ElementType>(GraphicStream<ElementType> graphicStream)
            where ElementType : struct 
        {
            mDescription = graphicStream.Description;

            var isIndexBuffer = mDescription.Usage == GraphicStreamUsage.Index;

            var dataStream = new DataStream(mDescription.Size, false, true);
            dataStream.WriteRange(graphicStream.Data.ToArray());

            // Important: when specifying initial buffer data like this, the buffer will
            // read from the current DataStream position; we must rewind the stream to 
            // the start of the data we just wrote.
            dataStream.Position = 0;

            var bufferDescription = new BufferDescription
            {
                BindFlags = isIndexBuffer ? BindFlags.IndexBuffer : BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = mDescription.Size,
                Usage = ResourceUsage.Default
            };

            mBuffer = new Buffer(mDevice, dataStream, bufferDescription);
            dataStream.Close();
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            if (mBuffer != null && !mBuffer.Disposed)
            {
                mBuffer.Dispose();
                mBuffer = null;
            }
        }
    }
}