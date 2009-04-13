using System;
using System.Collections.Generic;
using Afterglow.Graphics.GraphicStreams;
using OpenTK.Graphics;

namespace Afterglow.Graphics.Rendering
{
    /// <summary>
    /// Renderer for a single object which data is bound to OpenTK buffers
    /// </summary>
    public class OpenTKObjectRenderer : IObjectRenderer
    {
        private readonly IEnumerable<BufferBinding> mBufferBindings;

        private int mIndexCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKObjectRenderer"/> class.
        /// </summary>
        /// <param name="bufferBindings">The buffer bindings.</param>
        public OpenTKObjectRenderer(IEnumerable<BufferBinding> bufferBindings)
        {
            mBufferBindings = bufferBindings;

            foreach (var bufferBinding in mBufferBindings)
            {
                if(bufferBinding.Description.Usage == GraphicStreamUsage.Index)
                {
                    mIndexCount = bufferBinding.Description.Count;
                }
            }
        }

        /// <summary>
        /// Renders the object.
        /// </summary>
        public void Render()
        {
            foreach (var bufferBinding in mBufferBindings)
            {
                bufferBinding.Bind();
            }

            GL.DrawElements(BeginMode.Triangles, mIndexCount, DrawElementsType.UnsignedInt,
                IntPtr.Zero);
        }
    }
}