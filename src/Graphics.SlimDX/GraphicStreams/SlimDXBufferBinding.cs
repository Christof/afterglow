using SlimDX.Direct3D10;
using System.Diagnostics.Contracts;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Binds a <see cref="ISlimDXBuffer"/> to the input assembler.
    /// Also provides the ability to create an <see cref="InputElement"/>.
    /// </summary>
    public class SlimDXBufferBinding : BufferBinding
    {
        private readonly Device mDevice;

        private readonly ISlimDXBuffer mBuffer;

        private int mSlot = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXBufferBinding"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="buffer">The buffer.</param>
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
            CodeContract.Requires(mSlot != -1, "The slot must be set by calling CreateInputElement.");

            if (Description.Usage == GraphicStreamUsage.Index)
            {
                mDevice.InputAssembler.SetIndexBuffer(mBuffer.Buffer, Description.Format.ToFormat(), 0);
            }
            else
            {
                mDevice.InputAssembler.SetVertexBuffers(mSlot,
                    new VertexBufferBinding(mBuffer.Buffer, Description.ElementSize, 0));
            }
        }

        /// <summary>
        /// Creates the input element using the description.
        /// </summary>
        /// <remarks>
        /// This method sets the slot for the input assembler.
        /// It must be called before the first call to <see cref="Bind"/>.
        /// </remarks>
        /// <param name="slot">The input slot for binding.</param>
        /// <returns>An input element.</returns>
        public InputElement CreateInputElement(int slot)
        {
            mSlot = slot;

            return new InputElement(
                Description.Usage.ToSemantic(), 0,
                Description.Format.ToFormat(), 0, slot);
        }
    }
}