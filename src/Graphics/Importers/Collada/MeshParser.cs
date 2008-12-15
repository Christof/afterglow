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
    public class MeshParser
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
        /// Parses the given element as int array.
        /// </summary>
        /// <param name="intArray">The element containing the int array.</param>
        /// <returns>The int array.</returns>
        public static IEnumerable<int> ParseIntArray(XElement intArray)
        {
            return intArray.Value.Split(' ')
                .Select(s => Convert.ToInt32(s, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Finds the source for the given reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The source element for the given reference.</returns>
        public XElement FindSource(string reference)
        {
            string id = reference.Replace("#", "");

            var foundSources = mMeshElement.Elements(ColladaImporter.Namespace + "source")
                .Where(s => s.Attribute("id").Value == id);

            if (foundSources.Count() == 1)
                return foundSources.First();

            return null;
        }
    }
}