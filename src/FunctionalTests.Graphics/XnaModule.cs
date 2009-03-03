using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Graphics.Rendering;
using Afterglow.Graphics.Textures;
using Afterglow.Infrastructure;
using Afterglow.Input;
using Afterglow.Input.SlimDX;
using Afterglow.Math;
using Microsoft.Xna.Framework.Graphics;
using Ninject.Modules;

namespace Afterglow.Graphics
{
    public class XnaModule : Module
    {
        private readonly GraphicsDevice mDevice;

        public XnaModule(IRenderWindow renderWindow)
        {
            mDevice = renderWindow.DowncastTo<XnaRenderWindow>().Device;
        }

        public override void Load()
        {
            Bind<GraphicsDevice>().ToConstant(mDevice);

            Bind<IInputDevices>().To<SlimDXInputDevices>();

            Bind<IBufferService>().To<XnaBufferService>().InSingletonScope();
            Bind<IEffectCompiler>().To<XnaEffectCompiler>().InSingletonScope();
            Bind<IObjectRenderer>().To<XnaObjectRenderer>().InTransientScope();
            Bind<ITexture>().To<XnaTexture>().InTransientScope();

            Bind<SemanticEffectParameter<Matrix>>().To<XnaMatrixEffectParameter>().InTransientScope();
            Bind<SemanticEffectParameter<ITexture>>().To<XnaTextureEffectParameter>().InTransientScope();
        }
    }
}