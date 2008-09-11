using System;
using SlimDX.DXGI;
using SlimDX;
namespace TheNewEngine.Graphics.SlimDX
{
    public static class Extensions
    {
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