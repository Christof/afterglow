using System;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.GraphicStreams
{
    [TestFixture]
    public class Test_GraphicStreamFormatHelper
    {
        [Test]
        [Row(GraphicStreamFormat.Float, "Single")]
        [Row(GraphicStreamFormat.Vector2, "Vector2")]
        [Row(GraphicStreamFormat.Vector3, "Vector3")]
        [Row(GraphicStreamFormat.Vector4, "Vector4")]
        [Row(GraphicStreamFormat.Color4, "Color4")]
        [Row(GraphicStreamFormat.UInt, "UInt32")]
        [Row(GraphicStreamFormat.Int, "Int32")]
        [Row(GraphicStreamFormat.Float, "invalid", ExpectedException = typeof(ArgumentOutOfRangeException))]
        public void GetForTypeName(GraphicStreamFormat expectedFormat, string typeName)
        {
            Assert.AreEqual(expectedFormat, GraphicStreamFormatHelper.GetForTypeName(typeName));
        }
    }
}