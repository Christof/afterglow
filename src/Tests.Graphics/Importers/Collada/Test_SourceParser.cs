using System.Linq;
using System.Xml.Linq;
using MbUnit.Framework;

namespace TheNewEngine.Graphics
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
        public void Parse_for_Positions()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-Position")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var positions = sourceParser.Parse();

            Assert.IsNotNull(positions);
            var expected = new[]
            {
                new []{ 1f,  1f, 0f},
                new []{ 1f, -1f, 0f},
                new []{-1f, -1f, 0f},
                new []{-1f,  1f, 0f}
            };
            Assert.AreElementsEqual(expected, positions.ToArray());
        }

        [Test]
        public void Parse_for_Normals()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-Normals")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var normals = sourceParser.Parse();

            Assert.IsNotNull(normals);
            var expected = new[]
            {
                new []{0f, 0f, 1f},
                new []{0f, 0f, 1f},
            };
            Assert.AreElementsEqual(expected, normals.ToArray());
        }

        [Test]
        public void Parse_for_TexCoords()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-UV")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var texCoords = sourceParser.Parse();

            Assert.IsNotNull(texCoords);
            var expected = new[]
            {
                new []{0f, 0f},
                new []{1f, 0f},
                new []{1f, 1f},
                new []{0f, 0f},
                new []{1f, 1f},
                new []{0f, 1f}
            };
            Assert.AreElementsEqual(expected, texCoords.ToArray());
        }

        [Test]
        public void Parse_for_Colors()
        {
            var sourceElement = mMesh.Elements()
                .Where(s => s.Attribute("id").Value == "Plane-Geometry-color")
                .FirstOrDefault();
            Assert.IsNotNull(sourceElement);

            var sourceParser = new SourceParser(sourceElement);

            var texCoords = sourceParser.Parse();

            Assert.IsNotNull(texCoords);
            var expected = new[]
            {
                new []{1f, 1f, 1f, 1f},
                new []{1f, 1f, 1f, 1f},
                new []{1f, 1f, 1f, 1f},
                new []{1f, 1f, 1f, 1f},
                new []{1f, 1f, 1f, 1f},
                new []{1f, 1f, 1f, 1f},
            };
            Assert.AreElementsEqual(expected, texCoords.ToArray());
        }
    }
}