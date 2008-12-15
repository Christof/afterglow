using System.Xml.Linq;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.Importers.Collada
{
    public class Test_MeshParser
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

            var indices = MeshParser.ParseIntArray(pElement);
            var expected = new[]
            {
                0, 0, 0, 0, 3, 0, 1, 1, 2, 0, 2, 2,
                0, 1, 3, 3, 2, 1, 4, 4, 1, 1, 5, 5
            };

            Assert.AreElementsEqual(expected, indices);
        }

        [Test]
        public void FindSource()
        {
            var parser = new MeshParser(mMesh);

            var source = parser.FindSource("#Plane-Geometry-color");

            Assert.IsNotNull(source);
            Assert.AreEqual("Plane-Geometry-color", source.Attribute("id").Value);
        }

        [Test]
        public void FindSource_over_vertices_element()
        {
            var parser = new MeshParser(mMesh);

            var source = parser.FindSource("#Plane-Geometry-Vertex");

            Assert.IsNotNull(source);
            Assert.AreEqual("Plane-Geometry-Position", source.Attribute("id").Value);
        }
    }
}