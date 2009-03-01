using Afterglow.Graphics.Textures;
using SlimDX.Direct3D10;
using Afterglow.Infrastructure;

namespace Afterglow.Graphics.Effects
{
    /// <summary>
    /// Texture effect parameter.
    /// </summary>
    public class SlimDXTextureEffectParameter : SemanticEffectParameter<ITexture>
    {
        private readonly Device mDevice;

        private ShaderResourceView mShaderResourceView;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXMatrixEffectParameter"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="semanticName">Semantic name.</param>
        public SlimDXTextureEffectParameter(Device device, string semanticName)
            : base(semanticName)
        {
            mDevice = device;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXMatrixEffectParameter"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="semanticName">Semantic name.</param>
        /// <param name="texture">The texture.</param>
        public SlimDXTextureEffectParameter(
            Device device, string semanticName, ITexture texture)
            : this(device, semanticName)
        {
            mDevice = device;
            base.Value = texture;
            CreateShaderResourceView();
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public override ITexture Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                CreateShaderResourceView();
            }
        }

        private void CreateShaderResourceView()
        {
            mShaderResourceView = new ShaderResourceView(
                mDevice, base.Value.DowncastTo<SlimDXTexture>().Texture);
        }

        /// <summary>
        /// Sets the parameter on the given effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public override void SetParameterOn(IEffect effect)
        {
            ((SlimDXEffect)effect).Effect
                .GetVariableBySemantic(SemanticName)
                .AsResource()
                .SetResource(mShaderResourceView);
        }
    }
}