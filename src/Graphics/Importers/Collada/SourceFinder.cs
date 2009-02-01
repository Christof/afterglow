using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Finds source elements in a collada mesh element.
    /// </summary>
    internal class SourceFinder
    {
        private readonly XElement mMeshElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceFinder"/> class.
        /// </summary>
        /// <param name="meshElement">The mesh element.</param>
        public SourceFinder(XElement meshElement)
        {
            mMeshElement = meshElement;
        }
        
        /// <summary>
        /// Finds the source for the given reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The source element for the given reference.</returns>
        public XElement FindSource(string reference)
        {
            var foundSources = FindSources(reference);

            if (foundSources.Count() == 1)
            {
                return foundSources.First();
            }

            var verticesElement = mMeshElement
                .Elements(ColladaImporter.Namespace + "vertices")
                .Where(x => x.Attribute("id").Value == reference.Replace("#", string.Empty))
                .FirstOrDefault();

            if (verticesElement == null)
            {
                return null;
            }

            var inputElement = verticesElement
                .Element(ColladaImporter.Namespace + "input");
            
            string referenceFromVertices = inputElement.Attribute("source").Value;
            foundSources = FindSources(referenceFromVertices);

            return foundSources.First();
        }

        private IEnumerable<XElement> FindSources(string reference)
        {
            string id = reference.Replace("#", string.Empty);

            return mMeshElement.Elements(ColladaImporter.Namespace + "source")
                .Where(s => s.Attribute("id").Value == id);
        }
    }
}