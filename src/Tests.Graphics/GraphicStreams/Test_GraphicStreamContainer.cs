using MbUnit.Framework;
using Moq;
using System.Linq;

namespace TheNewEngine.Graphics.GraphicStreams
{
    [TestFixture]
    public class Test_GraphicStreamContainer
    {
        [Test]
        public void Constructor()
        {
            var container = new GraphicStreamContainer();

            Assert.IsNotNull(container);
        }

        [Test]
        public void Add()
        {
            var container = new GraphicStreamContainer();
            Assert.IsNotNull(container);
            Assert.AreEqual(0, container.Count());

            container.Add(new Mock<IGraphicStream>().Object);

            Assert.AreEqual(1, container.Count());
        }

        [Test]
        public void GetEnumerator()
        {
            var container = new GraphicStreamContainer();

            var streams = new[] { new Mock<IGraphicStream>().Object, new Mock<IGraphicStream>().Object };

            container.Add(streams[0]);
            container.Add(streams[1]);

            CollectionAssert.AreElementsEqual(streams, container);
        }

        [Test]
        public void Create_Returns_Stream_And_Adds_Stream()
        {
            var container = new GraphicStreamContainer();

            var usage = GraphicStreamUsage.Normal;
            var data = new[] { 1f, 2f, 3f };
            var stream = container.Create(usage, data);

            Assert.IsNotNull(stream);
            Assert.AreEqual(usage, stream.Usage);
            Assert.AreEqual(data, stream.Data);

            Assert.AreEqual(1, container.Count());
        }
    }
}