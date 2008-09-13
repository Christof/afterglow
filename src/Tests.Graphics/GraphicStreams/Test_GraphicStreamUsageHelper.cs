using System;
using MbUnit.Framework;

namespace TheNewEngine.Graphics
{
    [TestFixture]
    public class Test_GraphicStreamUsageHelper
    {
        [Test]
        [Row(GraphicStreamFormat.Float, "Single")]
        [Row(GraphicStreamFormat.Vector3, "Vector3")]
        [Row(GraphicStreamFormat.Color4, "Color4")]
        [Row(GraphicStreamFormat.Float, "invalid", ExpectedException = typeof(ArgumentOutOfRangeException))]
        public void GetForTypeName(GraphicStreamFormat format, string typeName)
        {
            Assert.AreEqual(format, GraphicStreamFormatHelper.GetForTypeName(typeName));
        }
    }
}