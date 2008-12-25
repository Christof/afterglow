using System.Collections.Generic;
using SlimDX;
using SlimDX.Direct3D10;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Resources;
using System.Diagnostics.Contracts;

namespace TheNewEngine.Graphics.SlimDX.GraphicStreams
{
    /// <summary>
    /// Contains buffer which build up all vertex data.
    /// </summary>
    public class BufferContainer : IResource
    {
        private readonly Device mDevice;

        private IResourceContainer<IGraphicStream> mGraphicStreamContainer;

        private List<InputElement> mInputElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferContainer"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public BufferContainer(Device device)
        {
            mDevice = device;
        }

        /// <summary>
        /// Called when the next frame is rendered.
        /// </summary>
        public void OnFrame()
        {
            mDevice.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            foreach (var graphicStream in mGraphicStreamContainer)
            {
                graphicStream.OnFrame();
            }
        }

        private IBuffer CreateBuffer(GraphicStreamFormat elementType)
        {
            switch (elementType)
            {
                case GraphicStreamFormat.Vector3:
                    return new Buffer<Math.Vector3>(mDevice);
                case GraphicStreamFormat.Color4:
                    return new Buffer<Color4>(mDevice);
                case GraphicStreamFormat.Int:
                    return new Buffer<int>(mDevice);
            }

            return null;
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="decoree">The decoree.</param>
        public void Load(IResource decoree)
        {
            CodeContract.Requires(decoree != null);

            var streamContainer = (IResourceContainer<IGraphicStream>)decoree;
            mGraphicStreamContainer = streamContainer;

            mInputElements = new List<InputElement>();

            int index = 0;

            foreach (var graphicStream in streamContainer)
            {
                var buffer = CreateBuffer(graphicStream.Format);

                buffer.Index = index++;

                if (graphicStream.Usage != GraphicStreamUsage.Index)
                {
                    var inputElement = new InputElement(
                        graphicStream.Usage.ToSemantic(), 0,
                        graphicStream.Format.ToFormat(), 0, buffer.Index);

                    mInputElements.Add(inputElement);
                }

                graphicStream.Load(buffer);
            }
        }

        /// <summary>
        /// Unloads the resource.
        /// </summary>
        public void Unload()
        {
            foreach (var graphicStream in mGraphicStreamContainer)
            {
                graphicStream.Unload();
            }
        }

        /// <summary>
        /// Gets the input elements.
        /// </summary>
        /// <value>The input elements.</value>
        public InputElement[] InputElements
        {
            get
            {
                return mInputElements.ToArray();
            }
        }
    }
}