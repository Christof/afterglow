using System;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Math;
using MbUnit.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaMatrix = Microsoft.Xna.Framework.Matrix;

namespace Afterglow.Graphics
{
    public class Test_XnaExtensions
    {
        [Test]
        [Row(GraphicStreamUsage.Position, VertexElementUsage.Position)]
        [Row(GraphicStreamUsage.Color, VertexElementUsage.Color)]
        [Row(GraphicStreamUsage.Normal, VertexElementUsage.Normal)]
        [Row(GraphicStreamUsage.Tangent, VertexElementUsage.Tangent)]
        [Row(GraphicStreamUsage.Binormal, VertexElementUsage.Binormal)]
        [Row(GraphicStreamUsage.TextureCoordinate, VertexElementUsage.TextureCoordinate)]
        [Row(GraphicStreamUsage.Index, VertexElementUsage.Fog, ExpectedException = typeof(InvalidOperationException))]
        public void ToSemantic(GraphicStreamUsage graphicStreamUsage, VertexElementUsage usage)
        {
            Assert.AreEqual(usage, graphicStreamUsage.ToVertexElementUsage());
        }

        [Test]
        [Row(GraphicStreamFormat.Float, VertexElementFormat.Single)]
        [Row(GraphicStreamFormat.Vector2, VertexElementFormat.Vector2)]
        [Row(GraphicStreamFormat.Vector3, VertexElementFormat.Vector3)]
        [Row(GraphicStreamFormat.Vector4, VertexElementFormat.Vector4)]
        [Row(GraphicStreamFormat.Color4, VertexElementFormat.Vector4)]
        [Row(GraphicStreamFormat.Int, VertexElementFormat.Unused, ExpectedException = typeof(InvalidOperationException))]
        [Row(GraphicStreamFormat.UInt, VertexElementFormat.Unused, ExpectedException = typeof(InvalidOperationException))]
        public void ToFormat(GraphicStreamFormat graphicStreamFormat, VertexElementFormat format)
        {
            Assert.AreEqual(format, graphicStreamFormat.ToFormat());
        }

        [Test]
        public void Vector_Conversion()
        {
            var vector = new Vector3(1, 2, 3);

            Microsoft.Xna.Framework.Vector3 xnaVector = vector.ToXna();

            Assert.AreEqual(vector.X, xnaVector.X);
            Assert.AreEqual(vector.Y, xnaVector.Y);
            Assert.AreEqual(vector.Z, xnaVector.Z);
        }

        [Test]
        public void Matrix_Conversion_ToXna()
        {
            var matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            XnaMatrix xnaMatrix = matrix.ToXna();

            Assert.AreEqual(matrix.R1C1, xnaMatrix.M11);
            Assert.AreEqual(matrix.R1C2, xnaMatrix.M12);
            Assert.AreEqual(matrix.R1C3, xnaMatrix.M13);
            Assert.AreEqual(matrix.R1C4, xnaMatrix.M14);

            Assert.AreEqual(matrix.R2C1, xnaMatrix.M21);
            Assert.AreEqual(matrix.R2C2, xnaMatrix.M22);
            Assert.AreEqual(matrix.R2C3, xnaMatrix.M23);
            Assert.AreEqual(matrix.R2C4, xnaMatrix.M24);

            Assert.AreEqual(matrix.R3C1, xnaMatrix.M31);
            Assert.AreEqual(matrix.R3C2, xnaMatrix.M32);
            Assert.AreEqual(matrix.R3C3, xnaMatrix.M33);
            Assert.AreEqual(matrix.R3C4, xnaMatrix.M34);

            Assert.AreEqual(matrix.R4C1, xnaMatrix.M41);
            Assert.AreEqual(matrix.R4C2, xnaMatrix.M42);
            Assert.AreEqual(matrix.R4C3, xnaMatrix.M43);
            Assert.AreEqual(matrix.R4C4, xnaMatrix.M44);
        }

        [Test]
        public void Matrix_Conversion_ToMath()
        {
            var xnaMatrix = new XnaMatrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            Matrix matrix = xnaMatrix.ToMath();

            Assert.AreEqual(xnaMatrix.M11, matrix.R1C1);
            Assert.AreEqual(xnaMatrix.M12, matrix.R1C2);
            Assert.AreEqual(xnaMatrix.M13, matrix.R1C3);
            Assert.AreEqual(xnaMatrix.M14, matrix.R1C4);

            Assert.AreEqual(xnaMatrix.M21, matrix.R2C1);
            Assert.AreEqual(xnaMatrix.M22, matrix.R2C2);
            Assert.AreEqual(xnaMatrix.M23, matrix.R2C3);
            Assert.AreEqual(xnaMatrix.M24, matrix.R2C4);

            Assert.AreEqual(xnaMatrix.M31, matrix.R3C1);
            Assert.AreEqual(xnaMatrix.M32, matrix.R3C2);
            Assert.AreEqual(xnaMatrix.M33, matrix.R3C3);
            Assert.AreEqual(xnaMatrix.M34, matrix.R3C4);

            Assert.AreEqual(xnaMatrix.M41, matrix.R4C1);
            Assert.AreEqual(xnaMatrix.M42, matrix.R4C2);
            Assert.AreEqual(xnaMatrix.M43, matrix.R4C3);
            Assert.AreEqual(xnaMatrix.M44, matrix.R4C4);
        }
    }
}