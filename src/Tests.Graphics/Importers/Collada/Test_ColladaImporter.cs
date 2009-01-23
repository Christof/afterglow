using MbUnit.Framework;

namespace TheNewEngine.Graphics.Importers.Collada
{
    public class Test_ColladaImporter
    {
        private const string COLLADA_SPHERE = "plane.dae"; 

        [Test]
        public void Constructor_awaits_path_to_a_collada_file()
        {
            var importer = new ColladaImporter(COLLADA_SPHERE);

            Assert.IsNotNull(importer);
        }

        [Test]
        public void GetFirstMesh()
        {
            var importer = new ColladaImporter(COLLADA_SPHERE);

            var container = importer.GetFirstMesh();

            Assert.IsNotNull(container);
        }
    }
}