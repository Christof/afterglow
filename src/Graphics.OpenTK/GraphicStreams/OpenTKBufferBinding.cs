using System;
using OpenTK.Graphics;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Binds a <see cref="IOpenTKBuffer"/> to the current context.
    /// </summary>
    public class OpenTKBufferBinding : BufferBinding
    {
        private readonly IOpenTKBuffer mBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKBufferBinding"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public OpenTKBufferBinding(IOpenTKBuffer buffer)
            : base(buffer.Description)
        {
            mBuffer = buffer;
        }

        /// <summary>
        /// Binds the buffer.
        /// </summary>
        public override void Bind()
        {
            if (Description.Usage == GraphicStreamUsage.Index)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mBuffer.Handle);
            }
            else if(Description.Usage == GraphicStreamUsage.Position)
            {
                GL.EnableClientState(EnableCap.VertexArray);

                GL.BindBuffer(BufferTarget.ArrayBuffer, mBuffer.Handle);
                GL.VertexPointer(3, VertexPointerType.Float, Description.ElementSizeInBytes,
                    IntPtr.Zero);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}