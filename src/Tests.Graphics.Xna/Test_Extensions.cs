using MbUnit.Framework;
using TheNewEngine.Math.Primitives;

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
        public void Matrix_Conversion()
        {
            var matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            Microsoft.Xna.Framework.Matrix xnaMatrix = matrix.ToXna();

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
    }
}