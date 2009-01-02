using System;

namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Provides helper functions for the <see cref="GraphicStreamFormat"/>-enum.
    /// </summary>
    public static class GraphicStreamFormatHelper
    {
        /// <summary>
        /// Gets the corresponding <see cref="GraphicStreamFormat"/> for the given type name.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The corresponding <see cref="GraphicStreamFormat"/> for the given type name.</returns>
        public static GraphicStreamFormat GetForTypeName(string typeName)
        {
            switch (typeName)
            {
                case "Vector2":
                    return GraphicStreamFormat.Vector2;

                case "Vector3":
                    return GraphicStreamFormat.Vector3;

                case "Vector4":
                    return GraphicStreamFormat.Vector4;

                case "Color4":
                    return GraphicStreamFormat.Color4;

                case "Single":
                    return GraphicStreamFormat.Float;

                case "UInt32":
                    return GraphicStreamFormat.UInt;

                case "Int32":
                    return GraphicStreamFormat.Int;

                default:
                    throw new ArgumentOutOfRangeException("typeName", typeName, "Invalid type name");
            }
        }
    }
}