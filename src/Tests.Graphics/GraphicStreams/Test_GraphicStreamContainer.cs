using MbUnit.Framework;
using Moq;
using System.Linq;
using TheNewEngine.Infrastructure;
using System.Collections;

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
        public void GetEnumerator_gernic_version()
        {
            var container = new GraphicStreamContainer();

            var streams = new[] { new Mock<IGraphicStream>().Object, new Mock<IGraphicStream>().Object };

            container.Add(streams[0]);
            container.Add(streams[1]);

            Assert.AreElementsEqual(streams, container);
        }

        [Test]
        public void GetEnumerator()
        {
            var container = new GraphicStreamContainer();

            var streams = new[] { new Mock<IGraphicStream>().Object, new Mock<IGraphicStream>().Object };

            container.Add(streams[0]);
            container.Add(streams[1]);

            var enumerator = (container as IEnumerable).GetEnumerator();

            Assert.IsNotNull(enumerator);
        }

        [Test]
        public void Create_Returns_Stream_And_Adds_Stream()
        {
            var container = new GraphicStreamContainer();

            var usage = GraphicStreamUsage.Normal;
            var data = new[] { 1f, 2f, 3f };
            var stream = container.Create(usage, data);

            Assert.IsNotNull(stream);
            Assert.AreEqual(usage, stream.Description.Usage);
            Assert.AreEqual(data, stream.Data);

            Assert.AreEqual(1, container.Count());
        }

        [Test]
        public void GetByUsage()
        {
            var container = new GraphicStreamContainer();

            var indices = new[] { 0, 1, 3 };
            container.Create(GraphicStreamUsage.Position, new[] { 1f, 2f, 3f });
            container.Create(GraphicStreamUsage.Index, indices);

            var stream = container.GetByUsage(GraphicStreamUsage.Index);

            Assert.IsNotNull(stream);
            Assert.AreEqual(GraphicStreamUsage.Index, stream.Description.Usage);
            Assert.AreElementsEqual(indices, stream.DowncastTo<GraphicStream<int>>().Data);
        }

        [Test]
        public void GetByUsage_returns_null_if_usage_is_not_found()
        {
            var container = new GraphicStreamContainer();

            Assert.IsNull(container.GetByUsage(GraphicStreamUsage.Normal));
        }
    }
}