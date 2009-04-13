using System;
using System.Linq;
using OpenTK.Graphics;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Implementation of a buffer with OpenTK.
    /// </summary>
    public class OpenTKBuffer : IOpenTKBuffer
    {
        /// <summary>
        /// value state for an invalid buffer handle
        /// </summary>
        public const int INVALID_HANDLE = -1;
        private int mHandle = INVALID_HANDLE;

        /// <summary>
        /// Loads the specified graphic stream into the buffer.
        /// </summary>
        /// <typeparam name="ElementType">The type of the element.</typeparam>
        /// <param name="graphicStream">The graphic stream.</param>
        public void Load<ElementType>(GraphicStream<ElementType> graphicStream) where ElementType : struct
        {
            Description = graphicStream.Description;

            GL.GenBuffers(1, out mHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            
            GL.BufferData(BufferTarget.ArrayBuffer,
                (IntPtr)graphicStream.Description.TotalSizeInBytes,
                graphicStream.Data.ToArray(),
                BufferUsageHint.StaticDraw);

            int size;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, 
                BufferParameterName.BufferSize, out size);
            if (graphicStream.Description.TotalSizeInBytes != size)
            {
                throw new ApplicationException("Vertex array not uploaded correctly");
            }
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            GL.DeleteBuffers(1, ref mHandle);
            mHandle = INVALID_HANDLE;
        }

        /// <summary>
        /// Gets the description for the elements in the buffer.
        /// </summary>
        /// <value>The description.</value>
        public GraphicStreamDescription Description { get; private set; }

        /// <summary>
        /// Gets the OpenTK specific handle of the buffer
        /// </summary>
        public int Handle 
        {
            get
            {
                return mHandle;
            }
        }
    }
}