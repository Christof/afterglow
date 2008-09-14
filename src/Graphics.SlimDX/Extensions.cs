using System;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using TheNewEngine.Graphics.GraphicStreams;

namespace TheNewEngine.Graphics.SlimDX
{
    /// <summary>
    /// This class contains extension methods which help to map engine types to
    /// the corresponding SlimDX types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns the semantic corresponding to the <see cref="GraphicStreamUsage"/>
        /// for the <see cref="InputElement"/>.
        /// </summary>
        /// <param name="graphicStreamUsage">The graphic stream usage.</param>
        /// <returns>Corresponding semantic.</returns>
        public static string ToSemantic(this GraphicStreamUsage graphicStreamUsage)
        {
            switch (graphicStreamUsage)
            {
                case GraphicStreamUsage.Position:
                    return "POSITION";

                case GraphicStreamUsage.Color:
                    return "COLOR";

                default:
                    throw new InvalidOperationException(
                        "Unable to convert the given graphic stream usage");
            }
        }

        /// <summary>
        /// Returns the <see cref="Format"/> for the <see cref="InputElement"/>
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>The corresponding <see cref="Format"/>.</returns>
        public static Format ToFormat(this GraphicStreamFormat format)
        {
            switch (format)
            {
                case GraphicStreamFormat.Vector3:
                    return Format.R32G32B32_Float;
                case GraphicStreamFormat.Color4:
                    return Format.R32G32B32A32_Float;
            }

            throw new InvalidOperationException();
        }
    }
}