using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Parses the mesh element of a collada file.
    /// </summary>
    internal class MeshParser
    {
        private readonly XElement mMeshElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshParser"/> class.
        /// </summary>
        /// <param name="meshElement">The mesh element.</param>
        public MeshParser(XElement meshElement)
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

            var inputElement = mMeshElement
                .Element(ColladaImporter.Namespace + "vertices")
                .Element(ColladaImporter.Namespace + "input");

            string referenceFromVertices = inputElement.Attribute("source").Value;
            foundSources = FindSources(referenceFromVertices);

            if (foundSources.Count() == 1)
            {
                return foundSources.First();
            }

            return null;
        }

        private IEnumerable<XElement> FindSources(string reference)
        {
            string id = reference.Replace("#", string.Empty);

            return mMeshElement.Elements(ColladaImporter.Namespace + "source")
                .Where(s => s.Attribute("id").Value == id);
        }
    }
}