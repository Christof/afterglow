namespace TheNewEngine.Graphics.Resources
{
    /// <summary>
    /// Resource decorator which has a name. The name could
    /// be used to identify the resource (e.g. a filename).
    /// </summary>
    public class NamedResourceDecorator : ResourceDecorator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedResourceDecorator"/> class.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        public NamedResourceDecorator(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <value>The name of the resource.</value>
        public string Name { get; private set; }
    }
}