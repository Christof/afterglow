using Afterglow.Graphics.Textures;
using Afterglow.Infrastructure;
using Microsoft.Xna.Framework.Graphics;

namespace Afterglow.Graphics.Effects
{
    /// <summary>
    /// Texture effect parameter.
    /// </summary>
    public class XnaTextureEffectParameter : SemanticEffectParameter<ITexture>
    {
        private readonly GraphicsDevice mDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTextureEffectParameter"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="semanticName">Semantic name.</param>
        public XnaTextureEffectParameter(GraphicsDevice device, string semanticName)
            : base(semanticName)
        {
            mDevice = device;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTextureEffectParameter"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="semanticName">Semantic name.</param>
        /// <param name="texture">The texture.</param>
        public XnaTextureEffectParameter(
            GraphicsDevice device, string semanticName, ITexture texture)
            : this(device, semanticName)
        {
            mDevice = device;
            base.Value = texture;
        }

        /// <summary>
        /// Sets the parameter on the given effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public override void SetParameterOn(IEffect effect)
        {
            effect.DowncastTo<XnaEffect>().Effect.Parameters
                .GetParameterBySemantic(SemanticName).SetValue(
                Value.DowncastTo<XnaTexture>().Texture);
        }
    }
}