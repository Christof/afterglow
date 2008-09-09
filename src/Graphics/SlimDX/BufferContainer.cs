namespace TheNewEngine.Graphics.SlimDX
{
    /// <summary>
    /// Contains buffer which build up all vertex data.
    /// </summary>
    public class BufferContainer : FrameResource
    {
        private GraphicStreamContainer mGraphicStreamContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferContainer"/> class.
        /// </summary>
        public BufferContainer()
        {
        }

        /// <summary>
        /// Sets the graphic stream container.
        /// </summary>
        /// <param name="graphicStreamContainer">The graphic stream container.</param>
        public void SetGraphicStreamContainer(GraphicStreamContainer graphicStreamContainer)
        {
            mGraphicStreamContainer = graphicStreamContainer;
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        public override void Load()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public override void Unload()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public override void OnFrame()
        {
            throw new System.NotImplementedException();
        }
    }
}