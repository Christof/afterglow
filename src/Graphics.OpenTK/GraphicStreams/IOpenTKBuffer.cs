namespace Afterglow.Graphics.GraphicStreams
{
    /// <summary>
    /// Storage for geometry data for OpenTK
    /// </summary>
    public interface IOpenTKBuffer : IBuffer
    {
        /// <summary>
        /// OpenTK specific handle of the buffer
        /// </summary>
        int Handle
        {
            get;
        }
    }
}