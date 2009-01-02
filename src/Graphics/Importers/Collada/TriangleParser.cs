using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Linq;
using System;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Parser for the triangle element.
    /// </summary>
    internal class TriangleParser
    {
        private readonly XElement mMeshElement;

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

        /// <summary>
        /// Parses all input elements in the triangle element.
        /// </summary>
        /// <returns>All input instances.</returns>
        public IEnumerable<Input> ParseInputs()
        {
            return mTriangleElement.Elements(ColladaImporter.Namespace + "input")
                .Select(inputElement => ParseInput(inputElement));
        }

        /// <summary>
        /// Parses an input element.
        /// </summary>
        /// <param name="element">The input element.</param>
        /// <returns>Input instance.</returns>
        public Input ParseInput(XElement element)
        {
            int offset = Convert.ToInt32(element.Attribute("offset").Value);
            string semantic = element.Attribute("semantic").Value;
            if (semantic == "VERTEX")
            {
                semantic = "POSITION";
            }

            string sourceReference = element.Attribute("source").Value;

            XElement source = new SourceFinder(mMeshElement).FindSource(sourceReference);

            return new Input(semantic, offset, source);
        }

        /// <summary>
        /// Parses the given element as int array.
        /// </summary>
        /// <param name="intArray">The element containing the int array.</param>
        /// <returns>The int array.</returns>
        public static IEnumerable<uint> ParseUIntArray(XElement intArray)
        {
            return intArray.Value.Split(' ')
                .Select(s => Convert.ToUInt32(s, CultureInfo.InvariantCulture));
        }

        public GraphicStreamContainer Parse()
        {
            var allIndices = ParseUIntArray(mTriangleElement.Element(
                ColladaImporter.Namespace + "p"));
            var container = new GraphicStreamContainer();

            var inputs = ParseInputs();
            foreach (var input in inputs)
            {
                var usage = SemanticHelper.GetUsageForSemantic(input.Semantic);
                var data = new SourceParser(input.SourceElement).Parse();

                var elementLength = data.First().Length;
                switch (elementLength)
                {
                    case 2:
                        container.Create(usage, data.Select(
                            element => new Vector2(element)).ToArray());
                        break;
                    case 3:
                        container.Create(usage, data.Select(
                            element => new Vector3(element)).ToArray());
                        break;
                    case 4:
                        container.Create(usage, data.Select(
                            element => new Vector4(element)).ToArray());
                        break;
                }
            }

            return container;
        }
    }
}