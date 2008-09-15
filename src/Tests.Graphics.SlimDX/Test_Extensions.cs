using System;
using MbUnit.Framework;
using TheNewEngine.Graphics.GraphicStreams;
using SlimDX.DXGI;

namespace TheNewEngine.Graphics.SlimDX
{
    public class Test_Extensions
    {
        [Test]
        [Row(GraphicStreamUsage.Position, "POSITION")]
        [Row(GraphicStreamUsage.Color, "COLOR")]
        [Row(GraphicStreamUsage.Normal, "NORMAL")]
        [Row(GraphicStreamUsage.Tangent, "TANGENT")]
        [Row(GraphicStreamUsage.Binormal, "BINORMAL")]
        [Row(GraphicStreamUsage.TextureCoordinate, "TEXCOORD")]
        [Row(GraphicStreamUsage.Index, null, ExpectedException = typeof(InvalidOperationException))]
        public void ToSemantic(GraphicStreamUsage graphicStreamUsage, string semantic)
        {
            Assert.AreEqual(semantic, graphicStreamUsage.ToSemantic());
        }

        [Test]
        [Row(GraphicStreamFormat.Float, Format.R32_Float)]
        [Row(GraphicStreamFormat.Vector3, Format.R32G32B32_Float)]
        [Row(GraphicStreamFormat.Color4, Format.R32G32B32A32_Float)]
        public void ToFormat(GraphicStreamFormat graphicStreamFormat, Format format)
        {
            Assert.AreEqual(format, graphicStreamFormat.ToFormat());
        }
    }
}