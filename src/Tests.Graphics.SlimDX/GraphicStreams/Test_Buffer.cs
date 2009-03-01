using MbUnit.Framework;
using Moq;
using SlimDX.Direct3D10;

namespace Afterglow.Graphics.GraphicStreams
{
    public class Test_Buffer
    {
        [Test]
        public void Constructor()
        {
            var buffer = new SlimDXBuffer(new Mock<Device>(DeviceCreationFlags.None).Object);

            Assert.IsNotNull(buffer);
        }

        [Test]
        public void Load_OnFrame_Unload_Cycle()
        {
            var deviceMock = new Mock<Device>(MockBehavior.Strict, DeviceCreationFlags.None);

            var data = new[] { 1f, 2f, 3f };
            var stream = new GraphicStream<float>(GraphicStreamUsage.Position, data);

            var buffer = new SlimDXBuffer(deviceMock.Object);

            buffer.Load(stream);

            buffer.Unload();
        }
    }
}