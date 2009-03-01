using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Afterglow.Infrastructure;

namespace Afterglow.Graphics
{
    /// <summary>
    /// Parser for source elements in a collada file.
    /// </summary>
    public class SourceParser
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
        /// Parses the source element.
        /// </summary>
        /// <returns>Parsed sequence of float-arrays.</returns>
        public IEnumerable<float[]> Parse()
        {
            var floats = ParseFloatArray(mSourceElement.Element(
                ColladaImporter.Namespace + "float_array"));

            var strideValue = mSourceElement
                .Element(ColladaImporter.Namespace + "technique_common")
                .Element(ColladaImporter.Namespace + "accessor")
                .Attribute("stride").Value;

            var stride = Convert.ToInt32(strideValue, CultureInfo.InvariantCulture);

            var vectors = floats.Slice(stride);

            return vectors;
        }

        private static IEnumerable<float> ParseFloatArray(XElement floatArray)
        {
            return floatArray.Value.Split(' ')
                .Select(s => Convert.ToSingle(s, CultureInfo.InvariantCulture));
        }
    }
}