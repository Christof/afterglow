namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Decorator for a frame resource.
    /// </summary>
    public class FrameResourceDecorator : FrameResource
    {
        private readonly FrameResource mDecoree;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameResourceDecorator"/> class.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        public FrameResourceDecorator(FrameResource decoree)
        {
            mDecoree = decoree;
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        public override void Load()
        {
            mDecoree.Load();
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public override void Unload()
        {
            mDecoree.Unload();
        }

        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public override void OnFrame()
        {
            mDecoree.OnFrame();
        }
    }
}