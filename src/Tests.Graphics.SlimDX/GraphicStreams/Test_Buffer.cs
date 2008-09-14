using MbUnit.Framework;
using Moq;
using SlimDX.Direct3D10;
using TheNewEngine.Graphics.GraphicStreams;

namespace TheNewEngine.Graphics.SlimDX.GraphicStreams
{
    public class Test_Buffer
    {
        [Test]
        public void Constructor()
        {
            var buffer = new Buffer<float>(new Mock<Device>(DeviceCreationFlags.None).Object);

            Assert.IsNotNull(buffer);
        }

        [Test]
        public void Load_OnFrame_Unload_Cycle()
        {
            var deviceMock = new Mock<Device>(MockBehavior.Strict, DeviceCreationFlags.None);

            var data = new[] { 1f, 2f, 3f };
            var stream = new GraphicStream<float>(GraphicStreamUsage.Position, data);

            var buffer = new Buffer<float>(deviceMock.Object);

            buffer.Load(stream);

            buffer.OnFrame();

            buffer.Unload();
        }
    }
}