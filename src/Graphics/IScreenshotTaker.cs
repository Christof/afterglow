namespace Afterglow.Graphics
{
    /// <summary>
    /// Classes which implement this interface can take a screenshot.
    /// </summary>
    public interface IScreenshotTaker
    {
        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="filename">The filename.</param>
        void TakeScreenshot(string filename);
    }
}