using System.Linq;
using System.Xml.Linq;
using MbUnit.Framework;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Infrastructure;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics
{
    public class Test_TriangleParser
    {
        private const string COLLAD_PLANE = "plane.dae";

        private XElement mMesh;

        [SetUp]
        public void Setup()
        {
            var doc = XDocument.Load(COLLAD_PLANE);
            mMesh = doc.Root
                .Element(ColladaImporter.Namespace + "library_geometries")
                .Element(ColladaImporter.Namespace + "geometry")
                .Element(ColladaImporter.Namespace + "mesh");
        }

        [Test]
        public void ParseIntArray()
        {
            var pElement = mMesh
                .Element(ColladaImporter.Namespace + "triangles")
                .Element(ColladaImporter.Namespace + "p");
            Assert.IsNotNull(pElement);

            var indices = TriangleParser.ParseUIntArray(pElement);
            var expected = new uint[]
            {
                0, 0, 0, 0, 3, 0, 1, 1, 2, 0, 2, 2,
                0, 1, 3, 3, 2, 1, 4, 4, 1, 1, 5, 5
            };

            Assert.AreElementsEqual(expected, indices);
        }

        [Test]
        public void ParseInput()
        {
            var triangleParser = new TriangleParser(mMesh);

            var normalInputElement = mMesh
                .Element(ColladaImporter.Namespace + "triangles")
                .Elements(ColladaImporter.Namespace + "input")
                .ToArray()[1];

            Input input = triangleParser.ParseInput(normalInputElement);

            Assert.IsNotNull(input);
            Assert.AreEqual("NORMAL", input.Semantic);
            Assert.AreEqual(1, input.Offset);
            Assert.IsNotNull(input.SourceElement);
        }

        [Test]
        public void ParseInput_for_VERTEX_input_element_semantic_should_return_POSITION_semantic()
        {
            var triangleParser = new TriangleParser(mMesh);

            var vertexInputElement = mMesh
                .Element(ColladaImporter.Namespace + "triangles")
                .Elements(ColladaImporter.Namespace + "input")
                .First();

            Input input = triangleParser.ParseInput(vertexInputElement);

            Assert.IsNotNull(input);
            Assert.AreEqual("POSITION", input.Semantic);
            Assert.AreEqual(0, input.Offset);
            Assert.IsNotNull(input.SourceElement);
        }

        [Test]
        public void ParseInputs()
        {
            var triangleParser = new TriangleParser(mMesh);

            var inputs = triangleParser.ParseInputs();

            Assert.AreEqual(4, inputs.Count());
        }

        [Test]
        public void Parse()
        {
            var triangleParser = new TriangleParser(mMesh);

            GraphicStreamContainer container = triangleParser.Parse();

            Assert.IsNotNull(container);
            Assert.AreEqual(5, container.Count());

            var expectedIndices = new uint[] { 0, 3, 2, 0, 2, 1 };
            var indexStream = container.GetByUsage(GraphicStreamUsage.Index)
                .DowncastTo<GraphicStream<uint>>();

            Assert.AreElementsEqual(expectedIndices, indexStream.Data);

            var expectedPositions = new[]
            {
                new Vector3(1f, 1f, 0f),
                new Vector3(1f, -1f, 0f),
                new Vector3(-1f, -1f, 0f),
                new Vector3(-1f, 1f, 0f)
            };
            var positionStream = container.GetByUsage(GraphicStreamUsage.Position)
                .DowncastTo<GraphicStream<Vector3>>();

            Assert.AreElementsEqual(expectedPositions, positionStream.Data);
        }
    }
}