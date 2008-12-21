namespace TheNewEngine.Graphics.Effects
{
    /// <summary>
    /// Base class for effect parameters which has a semantic-name
    /// and a value.
    /// </summary>
    /// <typeparam name="T">Type of the parameter's value</typeparam>
    public abstract class EffectParameter<T>
    {
        /// <summary>
        /// Gets or sets the semantic-name of the parameter.
        /// </summary>
        /// <value>The semantic-name.</value>
        public string SemanticName { get; private set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectParameter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="semanticName">Semantic-name.</param>
        protected EffectParameter(string semanticName)
        {
            SemanticName = semanticName;
        }

        /// <summary>
        /// Sets the parameter on the given effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public abstract void SetParameterOn(IEffect effect);
    }
}