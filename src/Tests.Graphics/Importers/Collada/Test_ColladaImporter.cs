using System.Diagnostics.Contracts;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.Importers.Collada
{
    public class Test_ColladaImporter
    {
        [Test]
        public void Constructor_awaits_path_to_a_collada_file()
        {
            var importer = new ColladaImporter("sphere.dae");

            Assert.IsNotNull(importer);
        }

        [Test]
        public void ParseSource()
        {
            
        }
    }

    internal class ColladaImporter
    {
        private readonly string mPath;

        public ColladaImporter(string path)
        {
            mPath = path;
        }
    }
}