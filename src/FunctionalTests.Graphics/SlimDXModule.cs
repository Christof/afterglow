using Afterglow.Graphics.GraphicStreams;
using Afterglow.Infrastructure;
using Afterglow.Input;
using Afterglow.Input.SlimDX;
using Ninject.Modules;
using SlimDX.Direct3D10;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.Rendering;
using Afterglow.Math;
using Afterglow.Graphics.Textures;

namespace Afterglow.Graphics
{
    public class SlimDXModule : Module
    {
        private readonly Device mDevice;

        public SlimDXModule(IRenderWindow renderWindow)
        {
            mDevice = renderWindow.DowncastTo<SlimDXRenderWindow>().Device;
        }

        public override void Load()
        {
            Bind<Device>().ToConstant(mDevice);

            Bind<IInputDevices>().To<SlimDXInputDevices>();

            Bind<IBufferService>().To<SlimDXBufferService>().InSingletonScope();
            Bind<IEffectCompiler>().To<SlimDXEffectCompiler>().InSingletonScope();
            Bind<IObjectRenderer>().To<SlimDXObjectRenderer>().InTransientScope();
            Bind<ITexture>().To<SlimDXTexture>().InTransientScope();

            Bind<SemanticEffectParameter<Matrix>>().To<SlimDXMatrixEffectParameter>().InTransientScope();
            Bind<SemanticEffectParameter<ITexture>>().To<SlimDXTextureEffectParameter>().InTransientScope();
        }
    }
}