using System.Diagnostics.Contracts;
using System.Xml.Linq;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Infrastructure;

namespace Afterglow.Graphics
{
    /// <summary>
    /// Importer for collada files.
    /// </summary>
    public class ColladaImporter
    {
        /// <summary>
        /// Collada namespace.
        /// </summary>
        public const string Namespace = "{http://www.collada.org/2005/11/COLLADASchema}";

        private readonly string mPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColladaImporter"/> class.
        /// </summary>
        /// <param name="path">Path to a collada file.</param>
        public ColladaImporter(string path)
        {
            CodeContract.Requires(!path.IsNullOrEmpty());

            mPath = path;
        }

        /// <summary>
        /// Gets the first mesh in the collada file.
        /// </summary>
        /// <returns>Parsed mesh.</returns>
        public GraphicStreamContainer GetFirstMesh()
        {
            var doc = XDocument.Load(mPath);
            var mesh = doc.Root
                .Element(Namespace + "library_geometries")
                .Element(Namespace + "geometry")
                .Element(Namespace + "mesh");

            return new TriangleParser(mesh).Parse();
        }
    }
}