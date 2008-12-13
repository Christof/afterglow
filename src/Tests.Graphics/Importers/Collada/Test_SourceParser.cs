using System.Linq;
using System.Xml.Linq;
using MbUnit.Framework;
using TheNewEngine.Math.Primitives;

namespace TheNewEngine.Graphics.Importers.Collada
{
    public class Test_SourceParser
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
        public void ParseVector3_for_Positions()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-Position")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var positions = sourceParser.ParseVector3();

            Assert.IsNotNull(positions);
            var expected = new[]
            {
                new Vector3(1, 1, 0),
                new Vector3(1, -1, 0),
                new Vector3(-1, -1, 0),
                new Vector3(-1, 1, 0)
            };
            Assert.AreElementsEqual(expected, positions);
        }

        [Test]
        public void ParseVector3_for_Normals()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-Normals")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var normals = sourceParser.ParseVector3();

            Assert.IsNotNull(normals);
            var expected = new[]
            {
                new Vector3(0, 0, 1),
                new Vector3(0, 0, 1),
            };
            Assert.AreElementsEqual(expected, normals);
        }

        [Test]
        public void ParseVector2_for_TexCoords()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-UV")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var texCoords = sourceParser.ParseVector2();

            Assert.IsNotNull(texCoords);
            var expected = new[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };
            Assert.AreElementsEqual(expected, texCoords);
        }
    }
}