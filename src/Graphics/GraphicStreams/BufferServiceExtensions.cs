using System;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Contains an extension method to make the adding of <see cref="BufferBinding"/>s easier.
    /// </summary>
    public static class BufferServiceExtensions
    {
        /// <summary>
        /// Creates a buffer for the given graphic stream.
        /// </summary>
        /// <param name="bufferService">The buffer service.</param>
        /// <param name="graphicStream">The graphic stream.</param>
        /// <returns>
        /// A buffer binding to enable the buffer each frame.
        /// </returns>
        public static BufferBinding CreateFor(this IBufferService bufferService, 
            IGraphicStream graphicStream)
        {
            switch (graphicStream.Description.Format)
            {
                case GraphicStreamFormat.UInt:
                    return bufferService.CreateFor((GraphicStream<uint>)graphicStream);

                case GraphicStreamFormat.Vector2:
                    return bufferService.CreateFor((GraphicStream<Vector2>)graphicStream);

                case GraphicStreamFormat.Vector3:
                    return bufferService.CreateFor((GraphicStream<Vector3>)graphicStream);

                case GraphicStreamFormat.Vector4:
                    return bufferService.CreateFor((GraphicStream<Vector4>)graphicStream);

                case GraphicStreamFormat.Color4:
                    return bufferService.CreateFor((GraphicStream<Vector4>)graphicStream);

                case GraphicStreamFormat.Float:
                    return bufferService.CreateFor((GraphicStream<float>)graphicStream);

                case GraphicStreamFormat.Int:
                    return bufferService.CreateFor((GraphicStream<int>)graphicStream);

                default:
                    throw new ArgumentException(string.Format(
                        "The format ({0}) of the given graphic stream is unknown.",
                        graphicStream.Description.Format));
            }
        }
    }
}