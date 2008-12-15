using MbUnit.Framework;
using TheNewEngine.Math;
using XnaMatrix = Microsoft.Xna.Framework.Matrix;

namespace TheNewEngine.Graphics.Xna
{
    public class Test_Extensions
    {
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