using SlimDX.Direct3D10;
using SlimDX;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Resources;

namespace TheNewEngine.Graphics.SlimDX.GraphicStreams
{
    /// <summary>
    /// Implementation of a frame resource with a <see cref="Buffer"/>.
    /// </summary>
    /// <typeparam name="ElementType">The type of the lement type.</typeparam>
    public class Buffer<ElementType> : IBuffer
        where ElementType : struct 
    {
        private readonly Device mDevice;

        private Buffer mBuffer;

        private int mElementSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buffer&lt;ElementType&gt;"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public Buffer(Device device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="frameResource">The frame resource.</param>
        public void Load(IFrameResource frameResource)
        {
            var graphicStream = (GraphicStream<ElementType>)frameResource;
            mElementSize = graphicStream.ElementSize;

            var dataStream = new DataStream(graphicStream.Size, false, true);
            dataStream.WriteRange(graphicStream.Data);

            // Important: when specifying initial buffer data like this, the buffer will
            // read from the current DataStream position; we must rewind the stream to 
            // the start of the data we just wrote.
            dataStream.Position = 0;

            var bufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = graphicStream.Size,
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

        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public void OnFrame()
        {
            mDevice.InputAssembler.SetVertexBuffers(Index,
                new VertexBufferBinding(mBuffer, mElementSize, 0));
        }
    }
}