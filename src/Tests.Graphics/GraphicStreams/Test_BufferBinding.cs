using MbUnit.Framework;
using Moq;

namespace Afterglow.Graphics.GraphicStreams
{
    public class Test_BufferBinding
    {
        [Test]
        public void Contains_a_GraphicStreamDescription()
        {
            var graphicStreamDescription = new GraphicStreamDescription();
            var mock = new Mock<BufferBinding>(graphicStreamDescription);

            Assert.AreEqual(graphicStreamDescription, mock.Object.Description);
        }
    }
}