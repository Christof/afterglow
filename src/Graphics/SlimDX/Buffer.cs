using SlimDX.Direct3D10;
using SlimDX;
namespace TheNewEngine.Graphics.SlimDX
{
    /// <summary>
    /// Implementation of a frame resource with a <see cref="Buffer"/>.
    /// </summary>
    /// <typeparam name="ElementType">The type of the lement type.</typeparam>
    public class Buffer<ElementType> : FrameResource
        where ElementType : struct 
    {
        private readonly Device mDevice;

        private GraphicStream<ElementType> mGraphicStream;

        private Buffer mBuffer;

        private int mIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buffer&lt;ElementType&gt;"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="index">The index.</param>
        public Buffer(Device device, int index)
        {
            mDevice = device;
            mIndex = index;
        }

        /// <summary>
        /// Sets the graphic stream.
        /// </summary>
        /// <param name="graphicStream">The graphic stream.</param>
        public void SetGraphicStream(GraphicStream<ElementType> graphicStream)
        {
            mGraphicStream = graphicStream;
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        public override void Load()
        {
            var dataStream = new DataStream(mGraphicStream.Size, false, true);
            dataStream.WriteRange(mGraphicStream.Data);

            // Important: when specifying initial buffer data like this, the buffer will
            // read from the current DataStream position; we must rewind the stream to 
            // the start of the data we just wrote.
            dataStream.Position = 0;

            var bufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = mGraphicStream.Size,
                Usage = ResourceUsage.Default
            };

            mBuffer = new Buffer(mDevice, dataStream, bufferDescription);
            dataStream.Close();
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public override void Unload()
        {
            if (mBuffer != null && mBuffer.Disposed)
            {
                mBuffer.Dispose();
                mBuffer = null;
            }
        }

        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public override void OnFrame()
        {
            mDevice.InputAssembler.SetVertexBuffers(mIndex,
                new VertexBufferBinding(mBuffer, mGraphicStream.ElementSize, 0));
        }
    }
}