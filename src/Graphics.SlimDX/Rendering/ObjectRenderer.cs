using TheNewEngine.Graphics.Effects;
using TheNewEngine.Infrastructure;
using TheNewEngine.Graphics.SlimDX.GraphicStreams;
using TheNewEngine.Graphics.GraphicStreams;
using SlimDXDevice = SlimDX.Direct3D10.Device;
using SlimDXEffect = SlimDX.Direct3D10.Effect;
using InputLayout = SlimDX.Direct3D10.InputLayout;
using Effect=TheNewEngine.Graphics.SlimDX.Effects.Effect;

namespace TheNewEngine.Graphics.SlimDX.Rendering
{
    public class ObjectRenderer : IObjectRenderer
    {
        private readonly SlimDXDevice mDevice;

        private readonly Effect mEffect;

        private readonly GraphicStreamContainer mContainer;

        private InputLayout mInputLayout;

        public ObjectRenderer(IRenderWindow renderWindow, IEffect effect, 
            GraphicStreamContainer container)
        {
            mDevice = renderWindow.DowncastTo<RenderWindow>().Device;
            mEffect = effect.DowncastTo<Effect>();
            mContainer = container;
        }

        public void Render()
        {
            if (mInputLayout == null)
                CreateInputLayout();

            mDevice.InputAssembler.SetInputLayout(mInputLayout);

            mContainer.OnFrame();

            var technique = mEffect.SlimDXEffect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);
            for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
            {
                pass.Apply();

                if (mContainer.IndexCount != 0)
                {
                    mDevice.DrawIndexed(mContainer.IndexCount, 0, 0);
                }
                else
                {
                    mDevice.Draw(mContainer.VertexCount, 0);
                }
            }
        }

        private void CreateInputLayout()
        {
            var technique = mEffect.SlimDXEffect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);

            mInputLayout = new InputLayout(mDevice,
                mContainer.Decoree.DowncastTo<BufferContainer>().InputElements,
                pass.Description.Signature);
        }
    }
}