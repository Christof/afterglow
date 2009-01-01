using SlimDX.Direct3D10;
using TheNewEngine.Graphics.Effects;
using TheNewEngine.Infrastructure;
using TheNewEngine.Graphics.GraphicStreams;
using System.Collections.Generic;
using System.Linq;

namespace TheNewEngine.Graphics.Rendering
{
    /// <summary>
    /// Object renderer for SlimDX.
    /// </summary>
    public class SlimDXObjectRenderer : IObjectRenderer
    {
        private readonly IEnumerable<BufferBinding> mBufferBindings;

        private readonly Device mDevice;

        private readonly SlimDXEffect mEffect;

        private readonly int mIndexCount;

        private readonly int mVertexCount;

        private InputLayout mInputLayout;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimDXObjectRenderer"/> class.
        /// </summary>
        /// <param name="renderWindow">The render window.</param>
        /// <param name="effect">The effect.</param>
        /// <param name="bufferBindings">The buffer bindings.</param>
        public SlimDXObjectRenderer(IRenderWindow renderWindow, IEffect effect, 
            IEnumerable<BufferBinding> bufferBindings)
        {
            mBufferBindings = bufferBindings;
            mDevice = renderWindow.DowncastTo<SlimDXRenderWindow>().Device;
            mEffect = effect.DowncastTo<SlimDXEffect>();

            foreach (var bufferBinding in bufferBindings)
            {
                var description = bufferBinding.Description;
                if (description.Usage == GraphicStreamUsage.Index)
                {
                    mIndexCount = description.Count;
                }

                if (description.Usage == GraphicStreamUsage.Position)
                {
                    mVertexCount = description.Count;
                }
            }
        }

        /// <summary>
        /// Renders the object.
        /// </summary>
        public void Render()
        {
            if (mInputLayout == null)
            {
                CreateInputLayout();
            }

            mDevice.InputAssembler.SetInputLayout(mInputLayout);
            mDevice.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);

            foreach (var bufferBinding in mBufferBindings)
            {
                bufferBinding.Bind();
            }

            var technique = mEffect.Effect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);
            for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
            {
                pass.Apply();

                if (mIndexCount != 0)
                {
                    mDevice.DrawIndexed(mIndexCount, 0, 0);
                }
                else
                {
                    mDevice.Draw(mVertexCount, 0);
                }
            }
        }

        private void CreateInputLayout()
        {
            var technique = mEffect.Effect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);

            var inputElements = mBufferBindings
                .Where(binding => binding.Description.Usage != GraphicStreamUsage.Index)
                .Select((binding, index) => binding.DowncastTo<SlimDXBufferBinding>()
                    .CreateInputElement(index))
                .ToArray();

            mInputLayout = new InputLayout(mDevice, inputElements,
                pass.Description.Signature);
        }
    }
}