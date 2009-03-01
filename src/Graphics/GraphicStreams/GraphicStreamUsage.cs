namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Defines usage of a graphic stream.
    /// </summary>
    public enum GraphicStreamUsage
    {
        /// <summary>
        /// The stream contains indices.
        /// </summary>
        Index,

        /// <summary>
        /// The stream contains positions.
        /// </summary>
        Position,

        /// <summary>
        /// The stream contains normals.
        /// </summary>
        Normal,

        /// <summary>
        /// The stream contains tangents.
        /// </summary>
        Tangent,

        /// <summary>
        /// The stream contains binormals.
        /// </summary>
        Binormal,

        /// <summary>
        /// The stream contains texture coordinates.
        /// </summary>
        TextureCoordinate,

        /// <summary>
        /// The stream contains colors.
        /// </summary>
        Color
    }
}