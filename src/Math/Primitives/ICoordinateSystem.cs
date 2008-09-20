namespace TheNewEngine.Math.Primitives
{
    /// <summary>
    /// Defines a coordinate system in 3d-space.
    /// </summary>
    public interface ICoordinateSystem
    {
        /// <summary>
        /// Gets the X axis.
        /// </summary>
        /// <value>The X axis.</value>
        Vector3 XAxis
        {
            get;
        }

        /// <summary>
        /// Gets the Y axis.
        /// </summary>
        /// <value>The Y axis.</value>
        Vector3 YAxis
        {
            get;
        }

        /// <summary>
        /// Gets the Z axis.
        /// </summary>
        /// <value>The Z axis.</value>
        Vector3 ZAxis
        {
            get;
        }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        /// <value>The origin.</value>
        Vector3 Origin
        {
            get;
        }
    }
}