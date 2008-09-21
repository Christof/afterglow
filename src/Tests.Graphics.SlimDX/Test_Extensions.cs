using System;
using MbUnit.Framework;
using TheNewEngine.Graphics.GraphicStreams;
using SlimDX.DXGI;
using TheNewEngine.Math.Primitives;
using SlimDXVector3 = SlimDX.Vector3;
using SlimDXMatrix = SlimDX.Matrix;

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

        [Test]
        public void Vector_Conversion()
        {
            var vector = new Vector3(1, 2, 3);

            SlimDXVector3 slimDXVector = vector.ToSlimDX();

            Assert.AreEqual(vector.X, slimDXVector.X);
            Assert.AreEqual(vector.Y, slimDXVector.Y);
            Assert.AreEqual(vector.Z, slimDXVector.Z);
        }

        [Test]
        public void Matrix_Conversion()
        {
            var matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            SlimDXMatrix slimDXMatrix = matrix.ToSlimDX();

            Assert.AreEqual(matrix.R1C1, slimDXMatrix.M11);
            Assert.AreEqual(matrix.R1C2, slimDXMatrix.M12);
            Assert.AreEqual(matrix.R1C3, slimDXMatrix.M13);
            Assert.AreEqual(matrix.R1C4, slimDXMatrix.M14);

            Assert.AreEqual(matrix.R2C1, slimDXMatrix.M21);
            Assert.AreEqual(matrix.R2C2, slimDXMatrix.M22);
            Assert.AreEqual(matrix.R2C3, slimDXMatrix.M23);
            Assert.AreEqual(matrix.R2C4, slimDXMatrix.M24);

            Assert.AreEqual(matrix.R3C1, slimDXMatrix.M31);
            Assert.AreEqual(matrix.R3C2, slimDXMatrix.M32);
            Assert.AreEqual(matrix.R3C3, slimDXMatrix.M33);
            Assert.AreEqual(matrix.R3C4, slimDXMatrix.M34);

            Assert.AreEqual(matrix.R4C1, slimDXMatrix.M41);
            Assert.AreEqual(matrix.R4C2, slimDXMatrix.M42);
            Assert.AreEqual(matrix.R4C3, slimDXMatrix.M43);
            Assert.AreEqual(matrix.R4C4, slimDXMatrix.M44);
        }
    }
}