using System.Collections.Generic;
using System.Linq;
using Afterglow.Graphics.Effects;
using Afterglow.Graphics.GraphicStreams;
using Microsoft.Xna.Framework.Graphics;
using Afterglow.Infrastructure;

namespace Afterglow.Graphics.Rendering
{
    /// <summary>
    /// Object renderer for Xna.
    /// </summary>
    public class XnaObjectRenderer : IObjectRenderer
    {
        private readonly IEnumerable<BufferBinding> mBufferBindings;

        private readonly GraphicsDevice mDevice;

        private readonly XnaEffect mEffect;

        private readonly int mIndexCount;

        private readonly int mVertexCount;

        private VertexDeclaration mVertexDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaObjectRenderer"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="effect">The effect.</param>
        /// <param name="bufferBindings">The buffer bindings.</param>
        public XnaObjectRenderer(GraphicsDevice device, IEffect effect, 
            IEnumerable<BufferBinding> bufferBindings)
        {
            mBufferBindings = bufferBindings;
            mDevice = device;
            mEffect = effect.DowncastTo<XnaEffect>();

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
            if (mVertexDeclaration == null)
            {
                CreateVertexDeclarations();
            }

            mDevice.VertexDeclaration = mVertexDeclaration;

            foreach (var bufferBinding in mBufferBindings)
            {
                bufferBinding.Bind();
            }

            var effect = mEffect.Effect;
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                if (mIndexCount != 0)
                {
                    mDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                        0, 0, mVertexCount, 0, mIndexCount / 3);
                }
                else
                {
                    mDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, mVertexCount / 3);
                }

                pass.End();
            }
            effect.End();
        }

        private void CreateVertexDeclarations()
        {
            var inputElements = mBufferBindings
                .Where(binding => binding.Description.Usage != GraphicStreamUsage.Index)
                .Select((binding, index) => binding.DowncastTo<XnaBufferBinding>()
                    .CreateInputElement(index))
                .ToArray();

            mVertexDeclaration = new VertexDeclaration(mDevice, inputElements);
        }
    }
}