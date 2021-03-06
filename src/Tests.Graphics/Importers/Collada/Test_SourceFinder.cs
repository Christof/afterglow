using System.Xml.Linq;
using MbUnit.Framework;

namespace Afterglow.Graphics.Importers.Collada
{
    public class Test_SourceFinder
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
        public void FindSource()
        {
            var parser = new SourceFinder(mMesh);

            var source = parser.FindSource("#Plane-Geometry-color");

            Assert.IsNotNull(source);
            Assert.AreEqual("Plane-Geometry-color", source.Attribute("id").Value);
        }

        [Test]
        public void FindSource_over_vertices_element()
        {
            var parser = new SourceFinder(mMesh);

            var source = parser.FindSource("#Plane-Geometry-Vertex");

            Assert.IsNotNull(source);
            Assert.AreEqual("Plane-Geometry-Position", source.Attribute("id").Value);
        }

        [Test]
        public void FindSource_for_not_existing_id()
        {
            var parser = new SourceFinder(mMesh);

            Assert.IsNull(parser.FindSource("#Not existing id"));
        }
    }
}