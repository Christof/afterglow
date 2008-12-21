using TheNewEngine.Graphics.Effects;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.SlimDX.Effects
{
    /// <summary>
    /// Matrix effect parameter.
    /// </summary>
    public class MatrixEffectParameter : EffectParameter<Matrix>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixEffectParameter"/> class.
        /// </summary>
        /// <param name="semanticName">Semantic-name.</param>
        public MatrixEffectParameter(string semanticName)
            : base(semanticName)
        {
        }

        /// <summary>
        /// Sets the parameter on the given effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public override void SetParameterOn(IEffect effect)
        {
            ((Effect)effect).SlimDXEffect
                .GetVariableBySemantic(SemanticName)
                .AsMatrix()
                .SetMatrix(Value.ToSlimDX());
        }
    }
}