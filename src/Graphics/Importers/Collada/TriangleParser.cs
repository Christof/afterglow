using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Parser for the triangle element.
    /// </summary>
    internal class TriangleParser
    {
        private XElement mMeshElement;

        private readonly XElement mTriangleElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleParser"/> class.
        /// </summary>
        /// <param name="meshElement">The triangle element.</param>
        public TriangleParser(XElement meshElement)
        {
            mMeshElement = meshElement;
            mTriangleElement = meshElement.Element(ColladaImporter.Namespace + "triangles");
        }

        public IEnumerable<Input> Parse()
        {
            mTriangleElement.Elements(ColladaImporter.Namespace + "input")
                .Select(e => e);

            return null;
        }

        public Input ParseInput(XElement element)
        {
            int offset = Convert.ToInt32(element.Attribute("offset").Value);
            string semantic = element.Attribute("semantic").Value;
            if (semantic == "VERTEX")
            {
                semantic = "POSITION";
            }

            string sourceReference = element.Attribute("source").Value;

            XElement source = new MeshParser(mMeshElement).FindSource(sourceReference);

            return new Input(semantic, offset, source);
        }
    }
}