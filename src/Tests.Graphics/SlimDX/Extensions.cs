using System;
namespace TheNewEngine.Graphics.SlimDX
{
    public static class Extensions
    {
        public static string ConvertToSemantic(this GraphicStreamUsage graphicStreamUsage)
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
    }
}