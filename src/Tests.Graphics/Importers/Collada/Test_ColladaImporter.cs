using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MbUnit.Framework;
using TheNewEngine.Infrastructure;
using System.IO;
using System.Xml.Linq;
using System;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Math.Primitives;
using System.Globalization;

namespace TheNewEngine.Graphics.Importers.Collada
{
    public class Test_ColladaImporter
    {
        private const string COLLADA_SPHERE = "sphere.dae";

        private const string COLLAD_PLANE = "plane.dae";

        [Test]
        public void Constructor_awaits_path_to_a_collada_file()
        {
            var importer = new ColladaImporter(COLLADA_SPHERE);

            Assert.IsNotNull(importer);
        }

        [Test]
        public void ParseSource()
        {
            var doc = XDocument.Load(COLLAD_PLANE);
            var root = doc.Root;
            var sourceElement = root
                .Element(ColladaImporter.Namespace + "library_geometries")
                .Element(ColladaImporter.Namespace + "geometry")
                .Element(ColladaImporter.Namespace + "mesh")
                .Elements().First();
            
            Assert.IsNotNull(sourceElement);

            var importer = new ColladaImporter(COLLAD_PLANE);

            var graphicStream = importer.ParseSource(sourceElement);

            Assert.IsNotNull(graphicStream);
            var expected = new[]
            {
                new Vector3(1, 1, 0),
                new Vector3(1, -1, 0),
                new Vector3(-1, -1, 0),
                new Vector3(-1, 1, 0)
            };
            Assert.AreElementsEqual(expected, (GraphicStream<Vector3>)graphicStream);
        }
    }

    internal class ColladaImporter
    {
        /// <summary>
        /// Collada namespace.
        /// </summary>
        public const string Namespace = "{http://www.collada.org/2005/11/COLLADASchema}";

        private readonly string mPath;

        public ColladaImporter(string path)
        {
            CodeContract.Requires(!path.IsNullOrEmpty());
            CodeContract.Requires(File.Exists(path));

            mPath = path;
        }

        public IGraphicStream ParseSource(XElement sourceElement)
        {
            return ParseVector3Source(sourceElement);
        }

        private GraphicStream<Vector3> ParseVector3Source(XElement sourceElement)
        {
            var floats = ParseFloatArray(sourceElement.Element(Namespace + "float_array"));

            var vectors = floats.Slice(3).Select(s => new Vector3(s[0], s[1], s[2]));

            return new GraphicStream<Vector3>(GraphicStreamUsage.Position, vectors.ToArray());
        }

        private IEnumerable<float> ParseFloatArray(XElement floatArray)
        {
            return floatArray.Value.Split(' ')
                .Select(s => Convert.ToSingle(s, CultureInfo.InvariantCulture));
        }
    }
}