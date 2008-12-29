using SlimDX.Direct3D10;

namespace TheNewEngine.Graphics.GraphicStreams
{
    public class SlimDXBufferBinding : BufferBinding
    {
        private readonly Device mDevice;

        private readonly ISlimDXBuffer mBuffer;

        private int mIndex;

        public SlimDXBufferBinding(Device device,
            ISlimDXBuffer buffer)
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
                mDevice.InputAssembler.SetIndexBuffer(mBuffer.Buffer, Description.Format.ToFormat(), 0);
            }
            else
            {
                mDevice.InputAssembler.SetVertexBuffers(mIndex,
                    new VertexBufferBinding(mBuffer.Buffer, Description.ElementSize, 0));
            }
        }

        public InputElement CreateInputElement(int index)
        {
            mIndex = index;

            return new InputElement(
                Description.Usage.ToSemantic(), 0,
                Description.Format.ToFormat(), 0, index);
        }
    }
}