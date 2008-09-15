using MbUnit.Framework;
using Moq;
using SlimDX.Direct3D10;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Graphics.Resources;

namespace TheNewEngine.Graphics.SlimDX.GraphicStreams
{
    public class Test_BufferContainer
    {
        [Test]
        public void Constructor()
        {
            var container = new BufferContainer(new Mock<Device>(DeviceCreationFlags.None).Object);

            Assert.IsNotNull(container);
        }

        [Test]
        public void Cylcle()
        {
            var deviceMock = new Mock<Device>(DeviceCreationFlags.None);

            var graphicStreamContainer = new GraphicStreamContainer();
            var streamMock = new Mock<IGraphicStream>();
            streamMock.ExpectGet(stream => stream.Usage).Returns(GraphicStreamUsage.Position);

            graphicStreamContainer.Add(streamMock.Object);

            var container = new BufferContainer(deviceMock.Object);

            container.Load(graphicStreamContainer);

            container.OnFrame();

            container.Unload();

            streamMock.VerifyAll();
            deviceMock.VerifyAll();
        }
    }
}