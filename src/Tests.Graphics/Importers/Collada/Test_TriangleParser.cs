using System.Linq;
using System.Xml.Linq;
using MbUnit.Framework;

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
        public void ParseInput()
        {
            var triangleParser = new TriangleParser(mMesh);

            var normalInputElement = mMesh
                .Element(ColladaImporter.Namespace + "triangles")
                .Elements(ColladaImporter.Namespace + "input")
                .ToArray()[1];

            var input = triangleParser.ParseInput(normalInputElement);

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

            var input = triangleParser.ParseInput(vertexInputElement);

            Assert.IsNotNull(input);
            Assert.AreEqual("POSITION", input.Semantic);
            Assert.AreEqual(0, input.Offset);
            Assert.IsNotNull(input.SourceElement);
        }
    }
}