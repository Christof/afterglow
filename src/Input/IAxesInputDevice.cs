namespace Afterglow.Input
{
    /// <summary>
    /// Interface for a input device which has buttons and three axes.
    /// </summary>
    public interface IAxesInputDevice : IInputDevice
    {
        /// <summary>
        /// Starts a configuration for an axis change.
        /// </summary>
        /// <param name="axis">The axis for which a configuration should be added.</param>
        /// <returns>An axis action.</returns>
        IAxisAction On(Axis axis);
    }
}