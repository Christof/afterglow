using System.Diagnostics.Contracts;
using System.IO;
using TheNewEngine.Infrastructure;

namespace TheNewEngine.Graphics
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
        /// <param name="path">The path.</param>
        public ColladaImporter(string path)
        {
            CodeContract.Requires(!path.IsNullOrEmpty());
            CodeContract.Requires(File.Exists(path));

            mPath = path;
        }
    }
}