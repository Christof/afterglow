using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using TheNewEngine.Infrastructure;
using TheNewEngine.Math.Primitives;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// Parser for source elements in a collada file.
    /// </summary>
    internal class SourceParser
    {
        private readonly XElement mSourceElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceParser"/> class.
        /// </summary>
        /// <param name="sourceElement">The source element.</param>
        public SourceParser(XElement sourceElement)
        {
            mSourceElement = sourceElement;
        }

        /// <summary>
        /// Parses a <see cref="Vector3"/> source.
        /// </summary>
        /// <returns>Parsed <see cref="Vector3"/>-array.</returns>
        public Vector3[] ParseVector3()
        {
            var floats = ParseFloatArray(mSourceElement.Element(
                ColladaImporter.Namespace + "float_array"));

            var vectors = floats.Slice(3).Select(s => new Vector3(s[0], s[1], s[2]));

            return vectors.ToArray();
        }

        private static IEnumerable<float> ParseFloatArray(XElement floatArray)
        {
            return floatArray.Value.Split(' ')
                .Select(s => Convert.ToSingle(s, CultureInfo.InvariantCulture));
        }

        public Vector2[] ParseVector2()
        {
            var floats = ParseFloatArray(mSourceElement.Element(
                ColladaImporter.Namespace + "float_array"));

            var vectors = floats.Slice(2).Select(s => new Vector2(s[0], s[1]));

            return vectors.ToArray();
        }
    }
}