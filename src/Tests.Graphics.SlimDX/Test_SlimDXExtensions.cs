using System;
using MbUnit.Framework;
using SlimDX.DXGI;
using TheNewEngine.Graphics.GraphicStreams;
using TheNewEngine.Math;
using SlimDXVector3 = SlimDX.Vector3;
using SlimDXMatrix = SlimDX.Matrix;

namespace TheNewEngine.Graphics.SlimDX
{
    public class Test_SlimDXExtensions
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
        [Row(GraphicStreamFormat.Vector2, Format.R32G32_Float)]
        [Row(GraphicStreamFormat.Vector3, Format.R32G32B32_Float)]
        [Row(GraphicStreamFormat.Vector4, Format.R32G32B32A32_Float)]
        [Row(GraphicStreamFormat.Color4, Format.R32G32B32A32_Float)]
        [Row(GraphicStreamFormat.Int, Format.R32_SInt)]
        [Row(GraphicStreamFormat.UInt, Format.R32_UInt)]
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
        public void Matrix_Conversion_ToSlimDX()
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

        [Test]
        public void Matrix_Conversion_ToMath()
        {
            var slimDXMatrix = new SlimDXMatrix
            {
                M11 = 11,
                M12 = 12,
                M13 = 13,
                M14 = 14,
                M21 = 21,
                M22 = 22,
                M23 = 23,
                M24 = 24,
                M31 = 31,
                M32 = 32,
                M33 = 33,
                M34 = 34,
                M41 = 41,
                M42 = 42,
                M43 = 43,
                M44 = 44
            };

            Matrix matrix = slimDXMatrix.ToMath();

            Assert.AreEqual(slimDXMatrix.M11, matrix.R1C1);
            Assert.AreEqual(slimDXMatrix.M12, matrix.R1C2);
            Assert.AreEqual(slimDXMatrix.M13, matrix.R1C3);
            Assert.AreEqual(slimDXMatrix.M14, matrix.R1C4);

            Assert.AreEqual(slimDXMatrix.M21, matrix.R2C1);
            Assert.AreEqual(slimDXMatrix.M22, matrix.R2C2);
            Assert.AreEqual(slimDXMatrix.M23, matrix.R2C3);
            Assert.AreEqual(slimDXMatrix.M24, matrix.R2C4);

            Assert.AreEqual(slimDXMatrix.M31, matrix.R3C1);
            Assert.AreEqual(slimDXMatrix.M32, matrix.R3C2);
            Assert.AreEqual(slimDXMatrix.M33, matrix.R3C3);
            Assert.AreEqual(slimDXMatrix.M34, matrix.R3C4);

            Assert.AreEqual(slimDXMatrix.M41, matrix.R4C1);
            Assert.AreEqual(slimDXMatrix.M42, matrix.R4C2);
            Assert.AreEqual(slimDXMatrix.M43, matrix.R4C3);
            Assert.AreEqual(slimDXMatrix.M44, matrix.R4C4);
        }
    }
}