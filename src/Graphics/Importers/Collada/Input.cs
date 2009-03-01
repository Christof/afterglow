using System.Xml.Linq;

namespace Afterglow.Graphics
{
    internal class Input
    {
        /// <summary>
        /// Gets the semantic.
        /// </summary>
        /// <value>The semantic.</value>
        public string Semantic { get; private set; }

        /// <summary>
        /// Gets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public int Offset { get; private set; }

        /// <summary>
        /// Gets the source element.
        /// </summary>
        /// <value>The source element.</value>
        public XElement SourceElement { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="semantic">The semantic.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sourceElement">The source element.</param>
        public Input(string semantic, int offset, XElement sourceElement)
        {
            Semantic = semantic;
            Offset = offset;
            SourceElement = sourceElement;
        }
    }
}