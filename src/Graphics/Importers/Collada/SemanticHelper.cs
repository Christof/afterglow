using System;
using TheNewEngine.Graphics.GraphicStreams;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Provides helper function to convert the semantic to a <see cref="GraphicStreamUsage"/>
    /// </summary>
    internal static class SemanticHelper
    {
        public static GraphicStreamUsage GetUsageForSemantic(string semantic)
        {
            switch (semantic)
            {
                case "POSITION":
                    return GraphicStreamUsage.Position;
                case "NORMAL":
                    return GraphicStreamUsage.Normal;
                case "TEXCOORD":
                    return GraphicStreamUsage.TextureCoordinate;
                case "COLOR":
                    return GraphicStreamUsage.Color;
                default:
                    throw new ArgumentException(string.Format(
                        "For the given semantic ({0}) is no GraphicStreamUsage defined.",
                        semantic));
            }
        }
    }
}