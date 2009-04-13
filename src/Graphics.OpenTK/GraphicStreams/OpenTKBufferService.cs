using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Implements a buffer service for OpenTK
    /// </summary>
    public class OpenTKBufferService : IBufferService
    {
        /// <summary>
        /// Creates a buffer for the given graphic stream.
        /// </summary>
        /// <typeparam name="ElementType">The type of each element in the graphic stream.</typeparam>
        /// <param name="graphicStream">The graphic stream.</param>
        /// <returns>A buffer binding to enable the buffer each frame.</returns>
        public BufferBinding CreateFor<ElementType>(GraphicStream<ElementType> graphicStream) where ElementType : struct
        {
            var buffer = new OpenTKBuffer();
            buffer.Load(graphicStream);

            return new OpenTKBufferBinding(buffer);
        }
    }
}
