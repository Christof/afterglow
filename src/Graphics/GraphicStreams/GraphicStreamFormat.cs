namespace TheNewEngine.Graphics.GraphicStreams
{
    /// <summary>
    /// Format of a graphic stream item.
    /// </summary>
    public enum GraphicStreamFormat
    {
        /// <summary>
        /// Vector2, which contains X and Y values.
        /// </summary>
        Vector2,

        /// <summary>
        /// Vector3, which contains X, Y and Z values.
        /// </summary>
        Vector3,

        /// <summary>
        /// Vector4, which contains X, Y, Z and W values.
        /// </summary>
        Vector4,

        /// <summary>
        /// Color4, which contains R, G, B and A values.
        /// </summary>
        Color4,

        /// <summary>
        /// Float value.
        /// </summary>
        Float,

        /// <summary>
        /// Int32 value.
        /// </summary>
        Int,
    }
}