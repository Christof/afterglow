using MbUnit.Framework;
using TheNewEngine.Graphics.GraphicStreams;
using System;

namespace TheNewEngine.Graphics
{
    public class Test_SemanticHelper
    {
        [Test]
        [Row("POSITION", GraphicStreamUsage.Position)]
        [Row("NORMAL", GraphicStreamUsage.Normal)]
        [Row("TEXCOORD", GraphicStreamUsage.TextureCoordinate)]
        [Row("COLOR", GraphicStreamUsage.Color)]
        [Row("undefined", GraphicStreamUsage.Position, ExpectedException = typeof(ArgumentException))]
        public void GetUsageForSemantic(string semantic, GraphicStreamUsage usage)
        {
            Assert.AreEqual(usage, SemanticHelper.GetUsageForSemantic(semantic));
        }
    }
}