using System.Linq;
using MbUnit.Framework;
using TheNewEngine.Infrastructure;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.GraphicStreams
{
    public class Test_GraphicStreamContainerSerialization
    {
        [Test]
        public void Serialize()
        {
            var container = new GraphicStreamContainer();
            var positions = new[] { Vector3.XAxis, Vector3.YAxis, Vector3.ZAxis };
            container.Create(GraphicStreamUsage.Position, positions);
            var texCoords = new[] { Vector2.XAxis, Vector2.YAxis };
            container.Create(GraphicStreamUsage.TextureCoordinate, texCoords);

            var filename = "test.txt";
            Serializer.SerializeBinary(container, filename);

            var deserializedContainer = Serializer.DeserializeBinary<GraphicStreamContainer>(filename);

            deserializedContainer.ShouldNotBeNull();
            deserializedContainer.Count().ShouldEqual(2);

            deserializedContainer.GetByUsage(GraphicStreamUsage.Position)
                .ShouldNotBeNull()
                .ShouldBe<GraphicStream<Vector3>>()
                .Data.ShouldEqual(positions);

            deserializedContainer.GetByUsage(GraphicStreamUsage.TextureCoordinate)
                .ShouldNotBeNull()
                .ShouldBe<GraphicStream<Vector2>>()
                .Data.ShouldEqual(texCoords);
        }
    }
}