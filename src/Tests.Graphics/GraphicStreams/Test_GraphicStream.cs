using Afterglow.Math;
using MbUnit.Framework;
using System.Collections;

namespace Afterglow.Graphics.GraphicStreams
{
    [TestFixture]
    public class Test_GraphicStream
    {
        [Test]
        public void Constructor_Sets_Usage_And_Data()
        {
            var usage = GraphicStreamUsage.Position;
            var data = new[] { 1f, 2f, 3f, 4f };

            var graphicStream = new GraphicStream<float>(usage, data);

            Assert.IsNotNull(graphicStream);
            Assert.AreEqual(usage, graphicStream.Description.Usage);
            Assert.AreEqual(data, graphicStream.Data);
        }

        [Test]
        public void Constructor_Sets_Size_And_ElementSize_And_Format_For_Float()
        {
            var data = new[] { 1f, 2f };
            var graphicStream = new GraphicStream<float>(GraphicStreamUsage.Color, data);

            var sizeOfFloat = sizeof(float);

            Assert.IsNotNull(graphicStream);
            Assert.AreEqual(sizeOfFloat, graphicStream.Description.ElementSizeInBytes);
            Assert.AreEqual(sizeOfFloat * data.Length, graphicStream.Description.TotalSizeInBytes);
            Assert.AreEqual(GraphicStreamFormat.Float, graphicStream.Description.Format);
        }

        [Test]
        public void Constructor_Sets_Size_And_ElementSize_And_Format_For_Vector3()
        {
            var data = new[] { new Vector3(), new Vector3() };
            var graphicStream = new GraphicStream<Vector3>(GraphicStreamUsage.Color, data);

            var sizeOfVector3 = 12;

            Assert.IsNotNull(graphicStream);
            Assert.AreEqual(sizeOfVector3, graphicStream.Description.ElementSizeInBytes);
            Assert.AreEqual(sizeOfVector3 * data.Length, graphicStream.Description.TotalSizeInBytes);
            Assert.AreEqual(GraphicStreamFormat.Vector3, graphicStream.Description.Format);
        }

        [Test]
        public void GetEnumerator_generic_version()
        {
            var data = new[] { 1f, 2f };
            var graphicStream = new GraphicStream<float>(GraphicStreamUsage.Color, data);

            Assert.IsNotNull(graphicStream);
            Assert.AreElementsEqual(data, graphicStream);
        }

        [Test]
        public void GetEnumerator()
        {
            var data = new[] { 1f, 2f };
            var graphicStream = new GraphicStream<float>(
                GraphicStreamUsage.Color, data) as IEnumerable;
            var enumerator = graphicStream.GetEnumerator();

            Assert.IsNotNull(enumerator);
        }
    }
}