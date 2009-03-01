using TheNewEngine.Infrastructure;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Effects
{
    /// <summary>
    /// Matrix effect parameter.
    /// </summary>
    public class XnaMatrixEffectParameter : SemanticEffectParameter<Matrix>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaMatrixEffectParameter"/> class.
        /// </summary>
        /// <param name="semanticName">Semantic name.</param>
        public XnaMatrixEffectParameter(string semanticName)
            : base(semanticName)
        {
        }
        /// <summary>
        /// Sets the parameter on the given effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public override void SetParameterOn(IEffect effect)
        {
            effect.DowncastTo<XnaEffect>()
                .Effect
                .Parameters
                .GetParameterBySemantic(SemanticName)
                .SetValue(Value.ToXna());
        }
    }
}