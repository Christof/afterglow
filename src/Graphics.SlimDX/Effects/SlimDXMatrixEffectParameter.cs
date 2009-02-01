using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Effects
{
    /// <summary>
    /// Matrix effect parameter.
    /// </summary>
    public class SlimDXMatrixEffectParameter : SemanticEffectParameter<Matrix>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXMatrixEffectParameter"/> class.
        /// </summary>
        /// <param name="semanticName">Semantic name.</param>
        public SlimDXMatrixEffectParameter(string semanticName)
            : base(semanticName)
        {
        }

        /// <summary>
        /// Sets the parameter on the given effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public override void SetParameterOn(IEffect effect)
        {
            ((SlimDXEffect)effect).Effect
                .GetVariableBySemantic(SemanticName)
                .AsMatrix()
                .SetMatrix(Value.ToSlimDX());
        }
    }
}